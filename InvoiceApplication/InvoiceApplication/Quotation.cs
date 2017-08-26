using InvoiceApplication.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InvoiceApplication
{
    public partial class Quotation : Form
    {
        public Quotation()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private void dataGridView1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {

                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                var qtycell = row.Cells[ColumnName.Qty].Value;
                var ratecell = row.Cells[ColumnName.Rate].Value;

                float qty;
                float cell;
                float lessDiscount;
                float CGSTRate;
                float SGSTRate;
                float.TryParse(Convert.ToString(row.Cells[ColumnName.LessDiscount].Value), out lessDiscount);
                float.TryParse(Convert.ToString(row.Cells[ColumnName.CGSTRate].Value), out CGSTRate);
                float.TryParse(Convert.ToString(row.Cells[ColumnName.SGSTRate].Value), out SGSTRate);

                if (qtycell != null && ratecell != null)
                    if (float.TryParse(qtycell.ToString(), out qty)
                        && float.TryParse(ratecell.ToString(), out cell))
                    {
                        var amount = qty * cell;
                        var taxAbleValue = amount - ((amount * lessDiscount) / 100);
                        var CGSTValue = ((taxAbleValue * CGSTRate) / 100);
                        var SGSTValue = ((taxAbleValue * SGSTRate) / 100);
                        row.Cells[ColumnName.Amount].Value = amount;
                        row.Cells[ColumnName.TaxableValue].Value = taxAbleValue;
                        row.Cells[ColumnName.CGSTAmount].Value = CGSTValue;
                        row.Cells[ColumnName.SGSTAmount].Value = SGSTValue;
                        row.Cells[ColumnName.Total].Value = taxAbleValue + SGSTValue + SGSTValue;


                    }

                decimal? totalAmount = 0;
                decimal? CGST = 0;
                decimal? SGST = 0;

                foreach (DataGridViewRow gridRow in dataGridView1.Rows)
                {
                    if (!gridRow.IsNewRow)
                    {

                        var product = new Product();


                        product.TaxableValue = Convert.ToDecimal(gridRow.Cells[ColumnName.TaxableValue].Value);
                        totalAmount += product.TaxableValue;

                        product.CGSTAmount = Convert.ToDecimal(gridRow.Cells[ColumnName.CGSTAmount].Value);
                        CGST += product.CGSTAmount;

                        product.SGSTAmount = Convert.ToDecimal(gridRow.Cells[ColumnName.SGSTAmount].Value);
                        SGST += product.SGSTAmount;
                    }
                }


                txtTotalbeforeTax.Text = Convert.ToString(totalAmount);
                txtTotalCGST.Text = Convert.ToString(CGST);
                txtTotalSGST.Text = Convert.ToString(SGST);
                txtTotalGST.Text = Convert.ToString(CGST + SGST);
                txtTotalAmountFinal.Text = Convert.ToString(totalAmount + CGST + SGST);
            }
        }

        private void groupBox2_Layout(object sender, LayoutEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtReceiverName.Text == string.Empty)
            {
                MessageBox.Show("Please enter Receiver name");
                return;
            }
            else if (txtReceiverState.Text == string.Empty)
            {
                MessageBox.Show("Please enter Receiver state");
                return;
            }

            var source = dataGridView1.DataSource;

            var invoice = new Invoice();
            invoice.ReverseCharge = txtReverseCharge.Text;
            invoice.Number = txtInvoiceNumber.Text;
            invoice.Date = txtInvoiceDate.Value;
            invoice.State = txtInvoiceState.Text;
            int stateCode;
            if (int.TryParse(Convert.ToString(txtInvoiceStateCode.Text), out stateCode))
            {
                invoice.StateCode = stateCode;
            }

            var transportaionMode = new TransportaionMode();
            transportaionMode.Mode = txtTransPortMode.Text;
            transportaionMode.VechicleNo = txtVehicleNumber.Text;
            transportaionMode.DateOfSupply = txtDateofSupply.Value;
            transportaionMode.PlaceOfSupply = txtPlaceofSupply.Text;
            int transStateCode;
            if (int.TryParse(Convert.ToString(txtTransCode.Text), out transStateCode))
            {
                transportaionMode.StateCode = transStateCode;
            }
            invoice.TransportaionMode = transportaionMode;


            var detailOfConsignee = new DetailOfConsignee();
            detailOfConsignee.Name = txtConsigneeName.Text;
            detailOfConsignee.Address = txtConsigneeAddress.Text;
            detailOfConsignee.GSTIN = txtConsigneeGSTIN.Text;
            detailOfConsignee.State = txtConsigneeState.Text;
            int consigneeStateCode;
            if (int.TryParse(Convert.ToString(txtConsigneeStateCode.Text), out consigneeStateCode))
            {
                detailOfConsignee.StateCode = consigneeStateCode;
            }
            invoice.DetailOfConsignee = detailOfConsignee;

            var customer = new Customer();
            customer.Name = txtReceiverName.Text;
            customer.Address = txtReceiverAddress.Text;
            customer.GSTIN = txtReceiverGSTIN.Text;
            customer.State = txtReceiverState.Text;
            int receiverStateCode;
            if (int.TryParse(Convert.ToString(txtReceiverstateCode.Text), out receiverStateCode))
            {
                customer.StateCode = receiverStateCode;
            }
            invoice.Customer = customer;

            var paymentDetail = new PaymentDetail();
            paymentDetail.IFSCCode = txtIFSC.Text;
            paymentDetail.AccountNumber = txtBankAccountNu.Text;
            invoice.PaymentDetail = paymentDetail;
            decimal? totalAmount = 0;
            decimal? CGST = 0;
            decimal? SGST = 0;


            bool productExits = false;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    productExits = true;
                    var product = new Product();
                    product.Name = Convert.ToString(row.Cells[ColumnName.Product].Value);
                    product.HSN = Convert.ToInt32(row.Cells[ColumnName.HSNACS].Value);
                    product.UOM = Convert.ToInt32(row.Cells[ColumnName.UOM].Value);
                    product.Quantity = Convert.ToInt32(row.Cells[ColumnName.Qty].Value);
                    product.Rate = Convert.ToDecimal(row.Cells[ColumnName.Rate].Value);
                    product.Amount = Convert.ToDecimal(row.Cells[ColumnName.Amount].Value);
                    product.Discount = Convert.ToDouble(row.Cells[ColumnName.LessDiscount].Value);
                    product.TaxableValue = Convert.ToDecimal(row.Cells[ColumnName.TaxableValue].Value);
                    totalAmount += product.TaxableValue;
                    product.CGSTRate = Convert.ToDouble(row.Cells[ColumnName.CGSTRate].Value);
                    product.CGSTAmount = Convert.ToDecimal(row.Cells[ColumnName.CGSTAmount].Value);
                    CGST += product.CGSTAmount;
                    product.SGSTRate = Convert.ToDouble(row.Cells[ColumnName.SGSTRate].Value);
                    product.SGSTAmount = Convert.ToDecimal(row.Cells[ColumnName.SGSTAmount].Value);
                    SGST += product.SGSTAmount;
                    product.IGSTRate = Convert.ToDouble(row.Cells[ColumnName.IGSTRate].Value);
                    product.IGSTAmount = Convert.ToDecimal(row.Cells[ColumnName.IGSTAmount].Value);
                    product.Total = Convert.ToDecimal(row.Cells[ColumnName.Total].Value);
                    if (string.IsNullOrEmpty(product.Name))
                    {
                        MessageBox.Show("Please enter Product Name");
                        return;
                    }
                    else if (product.HSN == null || product.HSN <= 0)
                    {
                        MessageBox.Show("Please enter HSN");
                        return;
                    }
                    else if (product.UOM == null || product.UOM <= 0)
                    {
                        MessageBox.Show("Please enter UOM.");
                        return;
                    }
                    else if (product.Quantity == null || product.Quantity.Value <= 0)
                    {
                        MessageBox.Show("Please enter Qty.");
                        return;
                    }
                    else if (product.Rate == null || product.Rate.Value <= 0)
                    {
                        MessageBox.Show("Please enter Rate");
                        return;
                    }

                    invoice.Products.Add(product);
                }
            }

            if (!productExits)
            {
                MessageBox.Show("Please enter product");
                return;
            }
            txtTotalbeforeTax.Text = Convert.ToString(totalAmount);
            txtTotalCGST.Text = Convert.ToString(CGST);
            txtTotalSGST.Text = Convert.ToString(SGST);
            txtTotalbeforeTax.Text = Convert.ToString(CGST + SGST);
            txtTotalCGST.Text = Convert.ToString(totalAmount + CGST);
            using (var context = new InvoiceEntities())
            {
                context.Invoices.Add(invoice);
                context.SaveChanges();
                MessageBox.Show("Invoice saved successfully");
            }
        }

        private void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[ColumnName.CGSTRate].Value = 9;
            e.Row.Cells[ColumnName.SGSTRate].Value = 9;
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (dataGridView1.CurrentCell.ColumnIndex == 1 || dataGridView1.CurrentCell.ColumnIndex == 2 || dataGridView1.CurrentCell.ColumnIndex == 3 || dataGridView1.CurrentCell.ColumnIndex == 4) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
        }
        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Quotation_Load(object sender, EventArgs e)
        {
            using (var context = new InvoiceEntities())
            {
                var invoiceNumber = context.Invoices.OrderByDescending(i => i.Id).Select(i => i.Number).FirstOrDefault();
                if (string.IsNullOrEmpty(invoiceNumber))
                {
                    invoiceNumber = "0";
                }
                txtInvoiceNumber.Text = (Convert.ToInt32(invoiceNumber) + 1).ToString();

            }
        }
    }
}
