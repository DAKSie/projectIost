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
        private InventoryView _inventoryView;
        private RegisterView _registerView;
        public LoginView(IIostService service)
        {
            _service = service;
            
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //string username = txtUser.Text.Trim();
            //string password = txtPass.Text;

            //var user = await _service.AuthenticateUserAsync(username, password);
            
            if (_inventoryView == null || _inventoryView.IsDisposed)
            {
                _inventoryView = new InventoryView();
            }
            this.Hide();
            _inventoryView.ShowDialog();
            _inventoryView.BringToFront();
            

        }

        private void btnExitLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSignin_Click(object sender, EventArgs e)
        {
            
            if (_registerView == null || _registerView.IsDisposed)
            {
                _registerView = new RegisterView();
            }
            this.Hide();
            
            _registerView.ShowDialog();
            _registerView.BringToFront();
            

        }
    }
}
