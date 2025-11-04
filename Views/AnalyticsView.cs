using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projectIost.Views
{
    public partial class AnalyticsView : Form
    {
        public AnalyticsView()
        {
            InitializeComponent();
            gunaScrollBar.BindingContainer = dgvSales;
            gunaScrollBar.AutoScroll = true;
        }

        private void guna2GradientPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
