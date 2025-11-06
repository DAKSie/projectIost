using Microsoft.Extensions.DependencyInjection;
using projectIost.Models;
using projectIost.Services;
using System;
using System.Windows.Forms;

namespace projectIost.Views
{
    public partial class RegisterView : Form
    {
        private readonly IIostService _service;
        private LoginView _loginView;

        public RegisterView()
        {
            InitializeComponent();
            _service = Program.ServiceProvider.GetRequiredService<IIostService>();
            

            // Make password textbox hide characters
            txtCPassword.UseSystemPasswordChar = true;
        }

        private async void btnCreateAcc_Click(object sender, EventArgs e)
        {
            string username = txtCUser.Text.Trim();
            string password = txtCPassword.Text.Trim();

            // Validation
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Please enter a username.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCUser.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter a password.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCPassword.Focus();
                return;
            }

            if (password.Length < 4)
            {
                MessageBox.Show("Password must be at least 4 characters long.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCPassword.Focus();
                return;
            }

            try
            {
                // Check if username already exists
                var existingUser = await _service.AuthenticateUserAsync(username, password);
                if (existingUser != null)
                {
                    MessageBox.Show("Username already exists. Please choose a different username.",
                        "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Create new user (default to non-admin)
                var newUser = new User
                {
                    User_name = username,
                    User_password = password,
                    user_isAdmin = false // Default to regular user
                };

                // Add user to database
                await _service.AddUserAsync(newUser);

                MessageBox.Show("Account created successfully! You can now login.",
                    "Registration Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear form and navigate to login
                ClearForm();
                NavigateToLogin();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to create account: {ex.Message}",
                    "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGotoLogin_Click(object sender, EventArgs e)
        {
            NavigateToLogin();
        }

        private void btnExitRegister_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void NavigateToLogin()
        {
            if (_loginView == null || _loginView.IsDisposed)
            {
                _loginView = Program.ServiceProvider.GetRequiredService<LoginView>();
            }

            this.Hide();
            _loginView.Show();
        }

        private void ClearForm()
        {
            txtCUser.Clear();
            txtCPassword.Clear();
            txtCUser.Focus();
        }

        // Optional: Enter key to submit form
        private void txtCPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnCreateAcc_Click(sender, e);
            }
        }

        // Optional: Enter key to move to password field
        private void txtCUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtCPassword.Focus();
            }
        }
    }
}