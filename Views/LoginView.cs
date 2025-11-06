using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
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

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim();
            string password = txtPass.Text;

            // Authenticate user
            var user = await _service.AuthenticateUserAsync(username, password);

            if (user != null)
            {
                // SUCCESS: Get InventoryView from DI container
                if (_inventoryView == null || _inventoryView.IsDisposed)
                {
                    _inventoryView = Program.ServiceProvider.GetRequiredService<InventoryView>();
                }
                Console.WriteLine("DEBUGDEBUGDEBUG DEBUGDEBUGDEBUG DEBUGDEBUGDEBUG DEBUGDEBUGDEBUG DEBUGDEBUGDEBUG DEBUGDEBUGDEBUG DEBUGDEBUGDEBUG DEBUGDEBUGDEBUG DEBUGDEBUGDEBUG");
                this.Hide();
                _inventoryView.Show();
                _inventoryView.BringToFront();
                
            }
            else
            {
                MessageBox.Show("Invalid username or password", "Login Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExitLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSignin_Click(object sender, EventArgs e)
        {
            // Get RegisterView from DI container
            if (_registerView == null || _registerView.IsDisposed)
            {
                _registerView = Program.ServiceProvider.GetRequiredService<RegisterView>();
            }

            this.Hide();
            _registerView.Show();
            _registerView.BringToFront();
        }
    }
}