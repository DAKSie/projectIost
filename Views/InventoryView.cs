using System;
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

        // Parameterless ctor kept for WinForms Designer.
        public InventoryView()
        {
            InitializeComponent();

            // Wire events so Designer-opened form has handlers attached.
            btnAdd.Click += btnAdd_Click;
            btnDelete.Click += btnDelete_Click;
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
            var sku = txtID.Text.Trim(); // adjust mapping if you intend txtID to be the numeric id

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

                // Clear inputs
                txtID.Clear();
                txtName.Clear();
                txtQuantity.Clear();
                txtCostPrice.Clear();
                txtRetailPrice.Clear();

                // Refresh grid to show new item
                await LoadItemsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to add item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                dgvInventory.DataSource = items;
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
    }
}
