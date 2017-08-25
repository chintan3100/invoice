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
                var qtycell = row.Cells["Qty"].Value;
                var ratecell = row.Cells["Rate"].Value;
                var lessDiscount = row.Cells["LessDiscount"].Value;
                long qty;
                long cell;
                if (qtycell!=null && ratecell!=null)
                if (long.TryParse(qtycell.ToString(), out qty)
                    && long.TryParse(ratecell.ToString(), out cell))
                {
                    row.Cells["Amount"].Value = qty * cell;
                }
            }
        }

        private void groupBox2_Layout(object sender, LayoutEventArgs e)
        {

        }
    }
}
