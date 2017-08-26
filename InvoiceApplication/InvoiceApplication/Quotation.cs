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
                        row.Cells[ColumnName.Total].Value = taxAbleValue - SGSTValue - SGSTValue;


                    }
            }
        }

        private void groupBox2_Layout(object sender, LayoutEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var source = dataGridView1.DataSource;

            var invoice = new Invoice();
            invoice.ReverseCharge = txtReverseCharge.Text;
            invoice.Number = "123";
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


            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    var product = new Product();
                    product.Name = Convert.ToString(row.Cells[ColumnName.Product].Value);
                    product.HSN = Convert.ToInt32(row.Cells[ColumnName.HSNACS].Value);
                    product.UOM = Convert.ToInt32(row.Cells[ColumnName.UOM].Value);
                    product.Quantity = Convert.ToInt32(row.Cells[ColumnName.Qty].Value);
                    product.Rate = Convert.ToDecimal(row.Cells[ColumnName.Rate].Value);
                    product.Amount = Convert.ToDecimal(row.Cells[ColumnName.Amount].Value);
                    product.Discount = Convert.ToDouble(row.Cells[ColumnName.LessDiscount].Value);
                    product.TaxableValue = Convert.ToDecimal(row.Cells[ColumnName.TaxableValue].Value);
                    product.CGSTRate = Convert.ToDouble(row.Cells[ColumnName.CGSTRate].Value);
                    product.CGSTAmount = Convert.ToDecimal(row.Cells[ColumnName.CGSTAmount].Value);
                    product.SGSTRate = Convert.ToDouble(row.Cells[ColumnName.SGSTRate].Value);
                    product.SGSTAmount = Convert.ToDecimal(row.Cells[ColumnName.SGSTAmount].Value);
                    product.IGSTRate = Convert.ToDouble(row.Cells[ColumnName.IGSTRate].Value);
                    product.IGSTAmount = Convert.ToDecimal(row.Cells[ColumnName.IGSTAmount].Value);
                    product.Total = Convert.ToDecimal(row.Cells[ColumnName.Total].Value);
                    invoice.Products.Add(product);
                }
            }

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
    }
}
