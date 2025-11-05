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
    public partial class AnalyticsView : Form
    {
        private readonly IIostService? _service;

        private InventoryView _inventoryView;

        private OrderView _orderView;

        private SupplyView _supplyView;

        private LoginView _loginView;
        public AnalyticsView()
        {
            InitializeComponent();
            gunaScrollBar.BindingContainer = dgvSales;
            gunaScrollBar.AutoScroll = true;
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

        private void btnOrder_Click(object sender, EventArgs e)
        {
            if (_orderView == null || _orderView.IsDisposed)
            {
                _orderView = new OrderView();
            }
            this.Hide();
            _orderView.ShowDialog();
            _orderView.BringToFront();
        }

        private void btnExitAnalytics_Click(object sender, EventArgs e)
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
