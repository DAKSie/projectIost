using Microsoft.Extensions.DependencyInjection;
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
            _service = Program.ServiceProvider.GetRequiredService<IIostService>();
            gunaScrollBar.BindingContainer = dgvSales;
            gunaScrollBar.AutoScroll = true;
        }

        private void NavigateTo<T>() where T : Form
        {
            var form = Program.ServiceProvider.GetRequiredService<T>();
            form.Show();
            this.Hide();
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            NavigateTo<InventoryView>();
        }

        private void btnSupply_Click(object sender, EventArgs e)
        {
            NavigateTo<SupplyView>();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            NavigateTo<OrderView>();
        }

        private void btnLogout2_Click(object sender, EventArgs e)
        {
            NavigateTo<LoginView>();
        }

        private void btnExitAnalytics_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /*
        private void btnInventory_Click(object sender, EventArgs e)
        {
            if (_inventoryView == null || _inventoryView.IsDisposed)
            {
                _inventoryView = Program.ServiceProvider.GetRequiredService<InventoryView>();
            }
            
            _inventoryView.Show();
            _inventoryView.BringToFront();
            this.Dispose();
        }

        private void btnSupply_Click(object sender, EventArgs e)
        {
            if (_supplyView == null || _supplyView.IsDisposed)
            {
                _supplyView = Program.ServiceProvider.GetRequiredService<SupplyView>();
            }
            
            _supplyView.Show();
            _supplyView.BringToFront();
            this.Dispose();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            if (_orderView == null || _orderView.IsDisposed)
            {
                _orderView = Program.ServiceProvider.GetRequiredService<OrderView>();
            }
            
            _orderView.Show();
            _orderView.BringToFront();
            this.Dispose();
        }

        

        private void btnLogout2_Click(object sender, EventArgs e)
        {
            if (_loginView == null || _loginView.IsDisposed)
            {
                _loginView = new LoginView(_service);
            }
           
            _loginView.ShowDialog();
            _loginView.BringToFront();
            this.Dispose();
        }
        */
    }
}
