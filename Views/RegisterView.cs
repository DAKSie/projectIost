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

namespace projectIost.Views
{
    public partial class RegisterView : Form
    {
        private readonly IIostService? _service;

        private LoginView _loginView;
        public RegisterView()
        {

            InitializeComponent();

        }


        private void btnExitRegister_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnCreateAcc_Click(object sender, EventArgs e)
        {
            if (_loginView == null || _loginView.IsDisposed)
            {
                _loginView = new LoginView(_service);
            }
            this.Hide();
            _loginView.ShowDialog();
            _loginView.BringToFront();
        }

        private void btnGotoLogin_Click(object sender, EventArgs e)
        {
            if (_loginView == null || _loginView.IsDisposed)
            {
                _loginView = new LoginView(_service);
            }
            this.Hide();
            _loginView.ShowDialog();
            _loginView.BringToFront();
        }
    }
}
