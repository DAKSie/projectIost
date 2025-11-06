using Microsoft.Extensions.DependencyInjection;
using projectIost.Models;
using projectIost.Services;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projectIost.Views
{
    public partial class InventoryView : Form
    {
        private readonly IIostService? _service;
        private readonly BindingSource _bindingSource = new();

        private SupplyView _supplyView;
        private OrderView _orderView;
        private AnalyticsView _analyticsView;
        private LoginView _loginView;

        public InventoryView()
        {
            InitializeComponent();
            
            txtID.ReadOnly = true;

            dgvInventory.DataSource = _bindingSource;
            dgvInventory.AllowUserToAddRows = false;

            dgvInventory.SelectionChanged += dgvInventory_SelectionChanged;
            dgvInventory.CellDoubleClick += dgvInventory_CellDoubleClick;
        }

        public InventoryView(IIostService service) : this()
        {
            _service = service;
            txtID.ReadOnly = true;

            dgvInventory.DataSource = _bindingSource;
            dgvInventory.AllowUserToAddRows = false;

            dgvInventory.SelectionChanged += dgvInventory_SelectionChanged;
            dgvInventory.CellDoubleClick += dgvInventory_CellDoubleClick;
        }

        private async void btnAdd_Click(object? sender, EventArgs e)
        {
            if (_service == null)
            {
                MessageBox.Show("Service not available. Construct this form via DI to enable database operations.",
                                "Service unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var name = txtName.Text.Trim();
            var sku = txtSKU.Text.Trim();   

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out var quantity)) quantity = 0;
            if (!decimal.TryParse(txtCostPrice.Text, out var cost)) cost = 0m;
            if (!decimal.TryParse(txtRetailPrice.Text, out var price)) price = 0m;

            var item = new Item
            {
                Item_SKU = sku,
                Item_name = name,
                Item_quantity = quantity,
                Item_cost = cost,
                Item_price = price
            };

            try
            {
                await _service.AddItemAsync(item);
                MessageBox.Show("Item added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                await LoadItemsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to add item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (_service == null)
            {
                MessageBox.Show("Service not available. Construct this form via DI to enable database operations.",
                                "Service unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtID.Text, out var itemId) || itemId == 0)
            {
                MessageBox.Show("Please select an item to edit (ID is missing or invalid).", "Validation",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var name = txtName.Text.Trim();
            var sku = txtSKU.Text.Trim();   

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out var quantity)) quantity = 0;
            if (!decimal.TryParse(txtCostPrice.Text, out var cost)) cost = 0m;
            if (!decimal.TryParse(txtRetailPrice.Text, out var price)) price = 0m;

            if (dgvInventory.CurrentRow?.DataBoundItem is Item boundItem)
            {
                boundItem.Item_SKU = sku;
                boundItem.Item_name = name;
                boundItem.Item_quantity = quantity;
                boundItem.Item_cost = cost;
                boundItem.Item_price = price;

                try
                {
                    await _service.UpdateItemAsync(boundItem);
                    MessageBox.Show("Item updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    await LoadItemsAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to update item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                var item = new Item
                {
                    Item_id = itemId,
                    Item_SKU = sku,
                    Item_name = name,
                    Item_quantity = quantity,
                    Item_cost = cost,
                    Item_price = price
                };

                try
                {
                    await _service.UpdateItemAsync(item);
                    MessageBox.Show("Item updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    await LoadItemsAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to update item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnDelete_Click(object? sender, EventArgs e)
        {
            if (_service == null)
            {
                MessageBox.Show("Service not available.", "Service unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvInventory.CurrentRow?.DataBoundItem is Item selected)
            {
                var confirm = MessageBox.Show("Delete selected item?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes) return;

                try
                {
                    await _service.DeleteItemAsync(selected.Item_id);
                    await LoadItemsAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to delete item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async Task LoadItemsAsync()
        {
            if (_service == null) return;

            try
            {
                var items = await _service.GetAllItemsAsync();

                dgvInventory.AutoGenerateColumns = true;
                dgvInventory.Columns.Clear();
                _bindingSource.DataSource = new BindingList<Item>(items);
                dgvInventory.DataSource = _bindingSource;
                dgvInventory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load items: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);
            await LoadItemsAsync();
        }

        private void dgvInventory_SelectionChanged(object? sender, EventArgs e)
        {
            PopulateSelectedRowToInputs();
        }

        private void dgvInventory_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            PopulateSelectedRowToInputs();
        }

        private void PopulateSelectedRowToInputs()
        {
            if (dgvInventory.CurrentRow?.DataBoundItem is Item selected)
            {
                txtID.Text = selected.Item_id.ToString();
                txtSKU.Text = selected.Item_SKU ?? string.Empty;
                txtName.Text = selected.Item_name ?? string.Empty;
                txtQuantity.Text = selected.Item_quantity.ToString();
                txtCostPrice.Text = selected.Item_cost.ToString();
                txtRetailPrice.Text = selected.Item_price.ToString();
            }
            else
            {
                ClearInputs();
            }
        }

        private void ClearInputs()
        {
            txtID.Clear();
            txtSKU.Clear();
            txtName.Clear();
            txtQuantity.Clear();
            txtCostPrice.Clear();
            txtRetailPrice.Clear();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /*
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

        private void btnAnalytics_Click(object sender, EventArgs e)
        {
            if (_analyticsView == null || _analyticsView.IsDisposed)
            {
                _analyticsView = Program.ServiceProvider.GetRequiredService<AnalyticsView>();
            }
            
            _analyticsView.Show();
            _analyticsView.BringToFront();
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

        private void NavigateTo<T>() where T : Form
        {
            var form = Program.ServiceProvider.GetRequiredService<T>();
            form.Show();
            this.Hide();
        }

        private void btnSupply_Click(object sender, EventArgs e)
        {
            NavigateTo<SupplyView>();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            NavigateTo<OrderView>();
        }

        private void btnAnalytics_Click(object sender, EventArgs e)
        {
            NavigateTo<AnalyticsView>();
        }

        private void btnLogout2_Click(object sender, EventArgs e)
        {
            NavigateTo<LoginView>();
        }
    }
}
