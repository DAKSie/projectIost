using projectIost.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using projectIost.Services;
using projectIost.Views;

namespace projectIost.Views
{
    public partial class LoginView : Form
    {
        private readonly IIostService _service;
        private readonly InventoryView _inventoryView;
        public LoginView(IIostService service, InventoryView inventoryView)
        {
            _service = service;
            _inventoryView = inventoryView;
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            _inventoryView.Show();
        }
    }
}
