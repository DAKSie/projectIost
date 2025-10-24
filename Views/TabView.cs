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
    public partial class TabView : Form
    {
        public TabView()
        {
            InitializeComponent();
        }

        private void InitializeTabs()
        {
            tabControl1.Dock = DockStyle.Fill;
            this.InventoryTab.Controls.Add(tabControl1);
        }
    }
}
