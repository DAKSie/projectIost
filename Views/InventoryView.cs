using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using projectIost.Services;
using projectIost.Models;

namespace projectIost.Views
{
    public partial class InventoryView : Form
    {
        // Service used to perform CRUD operations. Provided by DI at runtime.
        private readonly IIostService? _service;

        // Use a BindingSource so the DataGridView can be refreshed/managed easily.
        private readonly BindingSource _bindingSource = new();

        private SupplyView _supplyView;

        private OrderView _orderView;

        private AnalyticsView _analyticsView;

        private LoginView _loginView;

        // Parameterless ctor kept for WinForms Designer.
        public InventoryView()
        {
            InitializeComponent();

            txtID.ReadOnly = true; // Assuming ID is auto-generated.

            // Do NOT re-wire Designer events here. The Designer already wires btnAdd/btnEdit/btnClear/btnDelete.
            // Wiring them twice causes handlers to run twice.
            // Keep only UI-only wiring that Designer doesn't provide:
            dgvInventory.DataSource = _bindingSource;
            dgvInventory.AllowUserToAddRows = false;

            // Populate inputs when user selects a row.
            dgvInventory.SelectionChanged += dgvInventory_SelectionChanged;
            dgvInventory.CellDoubleClick += dgvInventory_CellDoubleClick;





        }

        // Runtime ctor for DI: IIostService will be injected automatically.
        public InventoryView(IIostService service) : this()
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        // Add button handler: validate inputs, create Item, call AddItemAsync, refresh grid.
        private async void btnAdd_Click(object? sender, EventArgs e)
        {
            if (_service == null)
            {
                MessageBox.Show("Service not available. Construct this form via DI to enable database operations.", "Service unavailable",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Basic validation/parsing
            var name = txtName.Text.Trim();
            var sku = txtSKU.Text.Trim(); // adjust mapping if you intend txtID to be the numeric id

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtSKU.Text, out var quantity)) quantity = 0;
            if (!decimal.TryParse(txtCostPrice.Text, out var cost)) cost = 0m;
            if (!decimal.TryParse(txtRetailPrice.Text, out var price)) price = 0m;

            var item = new Item
            {
                // Do not set Item_id for new entities; EF will generate it.
                Item_SKU = sku,
                Item_name = name,
                Item_quantity = quantity,
                Item_cost = cost,
                Item_price = price
            };

            try
            {
                // Persist the item via the application service
                await _service.AddItemAsync(item);

                MessageBox.Show("Item added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearInputs();

                // Refresh grid to show new item
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
                MessageBox.Show("Service not available. Construct this form via DI to enable database operations.", "Service unavailable",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtID.Text, out var itemId) || itemId == 0)
            {
                MessageBox.Show("Please select an item to edit (ID is missing or invalid).", "Validation",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Basic validation/parsing
            var name = txtName.Text.Trim();
            var sku = txtSKU.Text.Trim(); // adjust mapping if you intend txtID to be the numeric id

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtSKU.Text, out var quantity)) quantity = 0;
            if (!decimal.TryParse(txtCostPrice.Text, out var cost)) cost = 0m;
            if (!decimal.TryParse(txtRetailPrice.Text, out var price)) price = 0m;

            // Use the instance already bound to the grid (avoids attaching a second instance with same key)
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
                // Fallback: create new instance with the ID set (keeps previous behavior)
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


        // Optional delete handler showing use of DeleteItemAsync
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



        // Loads items from the service and binds them to the DataGridView.
        private async Task LoadItemsAsync()
        {
            if (_service == null) return;

            try
            {
                var items = await _service.GetAllItemsAsync();

                // Use a BindingList so the grid can more naturally reflect changes if needed.
                _bindingSource.DataSource = new BindingList<Item>(items);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load items: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // When the form is shown, refresh the grid.
        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);
            await LoadItemsAsync();
        }

        // Fill input controls from the currently selected grid row (if any)
        private void dgvInventory_SelectionChanged(object? sender, EventArgs e)
        {
            PopulateSelectedRowToInputs();
        }

        // Also allow double-click to edit quickly
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
                txtSKU.Text = selected.Item_quantity.ToString();
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
            txtSKU.Clear();
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
