using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using projectIost.Services;
using projectIost.Models;

namespace projectIost.Views
{
    public partial class InventoryView : Form
    {
        // Service used to perform CRUD operations. This is set when the form is created via DI.
        private readonly IIostService? _service;

        // Parameterless ctor kept for the WinForms Designer.
        public InventoryView()
        {
            InitializeComponent();
            // Wire the Add button event so the designer-created form works in both runtime and design time.
            btnAdd.Click += btnAdd_Click;
            btnDelete.Click += btnDelete_Click;
        }

        // Constructor for runtime use when resolving the form from DI.
        public InventoryView(IIostService service) : this()
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
        }

        // Event handler for the Add button.
        // This reads form inputs, validates them, constructs an Item and calls IIostService.AddItemAsync.
        // Use async void only for event handlers.
        private async void btnAdd_Click(object? sender, EventArgs e)
        {
            // If the form wasn't constructed with the service (e.g. in Designer), show a helpful error.
            if (_service == null)
            {
                MessageBox.Show("Service not available. Construct this form via DI to enable database operations.", "Service unavailable",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Basic validation/parsing
            var name = txtName.Text.Trim();
            var sku = txtID.Text.Trim(); // Using txtID for SKU field if your designer uses ID; adjust if needed.

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(sku))
            {
                MessageBox.Show("ID/SKU is recommended.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // not returning — SKU optional here
            }

            if (!int.TryParse(txtQuantity.Text, out var quantity))
            {
                quantity = 0;
            }

            if (!decimal.TryParse(txtCostPrice.Text, out var costPrice))
            {
                costPrice = 0m;
            }

            if (!decimal.TryParse(txtRetailPrice.Text, out var retailPrice))
            {
                retailPrice = 0m;
            }

            // Construct the domain model that the service expects.
            var item = new Item
            {
                // Do not set Item_id for new entities — let EF generate it.
                Item_SKU = sku,
                Item_name = name,
                Item_quantity = quantity,
                Item_cost = costPrice,
                Item_price = retailPrice
            };

            try
            {
                // Call into your application service to persist the new item.
                await _service.AddItemAsync(item);

                MessageBox.Show("Item added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear input fields after successful add.
                txtID.Clear();
                txtName.Clear();
                txtQuantity.Clear();
                txtCostPrice.Clear();
                txtRetailPrice.Clear();

                // Refresh grid to show the newly added item.
                await LoadItemsAsync();
            }
            catch (Exception ex)
            {
                // Surface error to the user; consider logging details in a real app.
                MessageBox.Show($"Failed to add item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Example Delete handler to show usage of the service; optional.
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

        // Optionally call LoadItemsAsync when the form is shown.
        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);
            await LoadItemsAsync();
        }
    }
}
