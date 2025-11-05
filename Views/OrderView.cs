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

    public partial class OrderView : Form
    {
        private readonly IIostService? _service;

        private InventoryView _inventoryView;

        private SupplyView _supplyView;

        private AnalyticsView _analyticsView;

        private LoginView _loginView;
        public OrderView()
        {
            InitializeComponent();
        }


        private void btnInventory_Click(object sender, EventArgs e)
        {
            if (_inventoryView == null || _inventoryView.IsDisposed)
            {
                _inventoryView = new InventoryView();
            }
            this.Hide();
            _inventoryView.ShowDialog();
            _inventoryView.BringToFront();
        }

        private void btnSupply_Click(object sender, EventArgs e)
        {
            if (_supplyView == null || _supplyView.IsDisposed)
            {
                _supplyView = new SupplyView();
            }
            this.Hide();
            _supplyView.Show();
            _supplyView.BringToFront();
        }

        private void btnAnalytics_Click(object sender, EventArgs e)
        {
            if (_analyticsView == null || _analyticsView.IsDisposed)
            {
                _analyticsView = new AnalyticsView();
            }
            this.Hide();
            _analyticsView.ShowDialog();
            _analyticsView.BringToFront();

        }


        private void btnExitOrder_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogout2_Click(object sender, EventArgs e)
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
