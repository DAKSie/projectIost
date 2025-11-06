using Microsoft.Extensions.DependencyInjection;
using projectIost.Models;
using projectIost.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projectIost.Views
{
    public partial class SupplyView : Form
    {
        private readonly IIostService? _service;
        private readonly BindingSource _inventoryBindingSource = new();
        private readonly BindingSource _supplyMasterBindingSource = new();
        private readonly BindingSource _supplyDetailBindingSource = new();

        private InventoryView _inventoryView;
        private OrderView _orderView;
        private AnalyticsView _analyticsView;
        private LoginView _loginView;

        public SupplyView()
        {
            InitializeComponent();
            SetupDGV();
        }

        public SupplyView(IIostService service) : this()
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        private void SetupDGV()
        {
            dgvSupply1.DataSource = _inventoryBindingSource;
            dgvSupply1.AllowUserToAddRows = false;

            dgvSupplyMaster.DataSource = _supplyMasterBindingSource;
            dgvSupplyMaster.AllowUserToAddRows = false;
            dgvSupplyMaster.SelectionChanged += dgvSupplyMaster_SelectionChanged;

            dgvSupply3.DataSource = _supplyDetailBindingSource;
            dgvSupply3.AllowUserToAddRows = false;
        }

        private async Task LoadInventoryAsync()
        {
            if (_service == null) return;

            try
            {
                var items = await _service.GetAllItemsAsync();

                dgvSupply1.Invoke((MethodInvoker)delegate
                {
                    dgvSupply1.AutoGenerateColumns = false;
                    dgvSupply1.Columns.Clear();

                    // manually added columns
                    dgvSupply1.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "Item_id",
                        HeaderText = "ID",
                        Width = 50
                    });
                    dgvSupply1.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "Item_SKU",
                        HeaderText = "SKU",
                        Width = 80
                    });
                    dgvSupply1.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "Item_name",
                        HeaderText = "Name",
                        Width = 150
                    });
                    dgvSupply1.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "Item_quantity",
                        HeaderText = "Quantity",
                        Width = 70
                    });
                    dgvSupply1.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "Item_cost",
                        HeaderText = "Cost",
                        Width = 70
                    });
                    dgvSupply1.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "Item_price",
                        HeaderText = "Price",
                        Width = 70
                    });

                    _inventoryBindingSource.DataSource = items;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load inventory: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadSuppliesAsync()
        {
            if (_service == null) return;

            try
            {
                var supplies = await _service.GetAllSuppliesAsync();

                dgvSupplyMaster.Invoke((MethodInvoker)delegate
                {
                    dgvSupplyMaster.AutoGenerateColumns = false;
                    dgvSupplyMaster.Columns.Clear();

                    dgvSupplyMaster.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "Supply_id",
                        HeaderText = "Supply ID",
                        Width = 70
                    });
                    dgvSupplyMaster.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "Date",
                        HeaderText = "Date",
                        Width = 100
                    });
                    dgvSupplyMaster.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "Supplier",
                        HeaderText = "Supplier",
                        Width = 170
                    });
                    dgvSupplyMaster.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "Total",
                        HeaderText = "Total",
                        Width = 80
                    });

                    _supplyMasterBindingSource.DataSource = new BindingList<Supply>(supplies);
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load supplies: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadSupplyDetailsAsync(int supplyId)
        {
            if (_service == null) return;

            try
            {
                var supplyDetails = await _service.GetSupplyDetailsBySupplyIdAsync(supplyId);

                dgvSupply3.Invoke((MethodInvoker)delegate
                {
                    dgvSupply3.AutoGenerateColumns = false;
                    dgvSupply3.Columns.Clear();

                    // manually added columns
                    dgvSupply3.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "Supply_Detail_id",
                        HeaderText = "Detail ID",
                        Width = 58
                    });
                    dgvSupply3.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "Supply_id",
                        HeaderText = "Supply ID",
                        Width = 58
                    });
                    dgvSupply3.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "Item_id",
                        HeaderText = "Item ID",
                        Width = 58
                    });
                    dgvSupply3.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "Quantity",
                        HeaderText = "Quantity",
                        Width = 70
                    });
                    dgvSupply3.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "Price",
                        HeaderText = "Price",
                        Width = 90
                    });
                    dgvSupply3.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "Subtotal",
                        HeaderText = "Subtotal",
                        Width = 90
                    });

                    _supplyDetailBindingSource.DataSource = new BindingList<Supply_Details>(supplyDetails);
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load supply details: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void dgvSupplyMaster_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSupplyMaster.CurrentRow?.DataBoundItem is Supply selectedSupply)
            {
                txtSupplyID.Text = selectedSupply.Supply_id.ToString();
                txtSupplyDate.Text = selectedSupply.Date.ToString("yyyy-MM-dd");
                txtSupplier.Text = selectedSupply.Supplier;

                try
                {
                    await LoadSupplyDetailsAsync(selectedSupply.Supply_id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load supply details: {ex.Message}");
                }
            }
        }

        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);
            await LoadAllDataAsync();
        }

        private bool _isLoading = false;

        private async Task LoadAllDataAsync()
        {
            try
            {
                // load sequentially, not concurrently
                await LoadInventoryAsync();
                await LoadSuppliesAsync(); 

                if (_supplyMasterBindingSource.Count > 0)
                {
                    var first = _supplyMasterBindingSource[0] as Supply;
                    if (first != null)
                    {
                        await LoadSupplyDetailsAsync(first.Supply_id); 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load data: {ex.Message}");
            }
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            if (_inventoryView == null || _inventoryView.IsDisposed)
            {
                _inventoryView = Program.ServiceProvider.GetRequiredService<InventoryView>();
            }
            this.Hide();
            _inventoryView.Show();
            _inventoryView.BringToFront();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            if (_orderView == null || _orderView.IsDisposed)
            {
                _orderView = Program.ServiceProvider.GetRequiredService<OrderView>();
            }
            this.Hide();
            _orderView.Show();
            _orderView.BringToFront();
        }

        private void btnAnalytics_Click(object sender, EventArgs e)
        {
            if (_analyticsView == null || _analyticsView.IsDisposed)
            {
                _analyticsView = Program.ServiceProvider.GetRequiredService<AnalyticsView>();
            }
            this.Hide();
            _analyticsView.Show();
            _analyticsView.BringToFront();
        }

        private void btnExitSupply_Click(object sender, EventArgs e)
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

        private async void btnAddSupply_Click(object sender, EventArgs e)
        {
            if (_service == null)
            {
                MessageBox.Show("Service not available", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // input validation
            if (string.IsNullOrWhiteSpace(txtSupplier.Text) || string.IsNullOrWhiteSpace(txtSupplyDate.Text))
            {
                MessageBox.Show("Please enter Supplier and Date", "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // parse date
                if (!DateTime.TryParse(txtSupplyDate.Text, out DateTime supplyDate))
                {
                    MessageBox.Show("Please enter a valid date format (YYYY-MM-DD)", "Validation Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // create new supply
                var newSupply = new Supply
                {
                    Date = supplyDate,
                    Supplier = txtSupplier.Text.Trim(),
                    Total = 0, 
                    User_id = 1 
                };

                await _service.AddSupplyAsync(newSupply);

                MessageBox.Show("Supply created successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                // refresh supplies list
                await LoadSuppliesAsync();

                // clear fields
                btnClearSupply_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to add supply: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEditSupply_Click(object sender, EventArgs e)
        {
            if (_service == null || string.IsNullOrWhiteSpace(txtSupplyID.Text))
            {
                MessageBox.Show("Please select a supply to edit", "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // get selected supply ID
                if (!int.TryParse(txtSupplyID.Text, out int supplyId))
                {
                    MessageBox.Show("Invalid Supply ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // validate input
                if (string.IsNullOrWhiteSpace(txtSupplier.Text) || string.IsNullOrWhiteSpace(txtSupplyDate.Text))
                {
                    MessageBox.Show("Please enter Supplier and Date", "Validation Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // parse date
                if (!DateTime.TryParse(txtSupplyDate.Text, out DateTime supplyDate))
                {
                    MessageBox.Show("Please enter a valid date format (YYYY-MM-DD)", "Validation Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // get existing supply
                var existingSupply = await _service.GetSupplyByIdAsync(supplyId);
                if (existingSupply == null)
                {
                    MessageBox.Show("Supply not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // update supply info
                existingSupply.Date = supplyDate;
                existingSupply.Supplier = txtSupplier.Text.Trim();

                await _service.UpdateSupplyAsync(existingSupply);

                MessageBox.Show("Supply updated successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                // refresh supplies list
                await LoadSuppliesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to update supply: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearSupply_Click(object sender, EventArgs e)
        {
            // clear form 
            txtSupplyID.Clear();
            txtSupplyDate.Clear();
            txtSupplier.Clear();

            _supplyDetailBindingSource.DataSource = new BindingList<Supply_Details>();
        }

        private async void btnDelSupply_Click(object sender, EventArgs e)
        {
            if (_service == null || string.IsNullOrWhiteSpace(txtSupplyID.Text))
            {
                MessageBox.Show("Please select a supply to delete", "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // get selected supply ID
                if (!int.TryParse(txtSupplyID.Text, out int supplyId))
                {
                    MessageBox.Show("Invalid Supply ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // confirm deletion
                var result = MessageBox.Show(
                    $"Are you sure you want to delete supply #{supplyId}? This will also delete all supply details.",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // delete supply details
                    await _service.DeleteSupplyDetailsBySupplyIdAsync(supplyId);

                    // then delete the supply
                    await _service.DeleteSupplyAsync(supplyId);

                    MessageBox.Show("Supply deleted successfully!", "Success",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // refresh supplies list and clear fields
                    await LoadSuppliesAsync();
                    btnClearSupply_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to delete supply: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnAddItem_Click(object sender, EventArgs e)
        {
            if (_service == null) return;

            // input validation
            if (!int.TryParse(txtSupplyID.Text, out int supplyId) ||
                dgvSupply1.CurrentRow?.DataBoundItem is not Item selectedItem ||
                !int.TryParse(txtQuantitySupply.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Please select a supply, item, and enter valid quantity", "Validation Error");
                return;
            }

            try
            {
                // add supply detail
                var supplyDetail = new Supply_Details
                {
                    Supply_id = supplyId,
                    Item_id = selectedItem.Item_id,
                    Quantity = quantity,
                    Price = selectedItem.Item_cost,
                    Subtotal = selectedItem.Item_cost * quantity
                };

                await _service.AddSupplyDetailAsync(supplyDetail);

                // update inventory
                selectedItem.Item_quantity += quantity;
                await _service.UpdateItemAsync(selectedItem);

                // update supply total
                var supply = await _service.GetSupplyByIdAsync(supplyId);
                if (supply != null)
                {
                    var details = await _service.GetSupplyDetailsBySupplyIdAsync(supplyId);
                    supply.Total = details.Sum(sd => sd.Subtotal);
                    await _service.UpdateSupplyAsync(supply);
                }

                // refresh UI 
                await LoadInventoryAsync();
                await LoadSuppliesAsync();
                await LoadSupplyDetailsAsync(supplyId);

                txtQuantitySupply.Clear();
                MessageBox.Show($"Added {quantity} {selectedItem.Item_name} to supply");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async Task UpdateSupplyTotal(int supplyId)
        {
            var supply = await _service.GetSupplyByIdAsync(supplyId);
            if (supply != null)
            {
                var details = await _service.GetSupplyDetailsBySupplyIdAsync(supplyId);
                supply.Total = details.Sum(sd => sd.Subtotal);
                await _service.UpdateSupplyAsync(supply);
            }
        }

        private async Task RefreshData(int supplyId)
        {
            await LoadInventoryAsync();
            await LoadSuppliesAsync();
            await LoadSupplyDetailsAsync(supplyId);
        }

        private async void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (_service == null) return;

            if (dgvSupply3.CurrentRow?.DataBoundItem is not Supply_Details selectedDetail)
            {
                MessageBox.Show("Please select an item to remove from the supply");
                return;
            }

            try
            {
                // get the item to adjust inventory
                var item = await _service.GetItemByIdAsync(selectedDetail.Item_id);
                if (item != null)
                {
                    // remove the quantity from inventory
                    item.Item_quantity -= selectedDetail.Quantity;
                    await _service.UpdateItemAsync(item);
                }

                // remove the supply detail
                await _service.DeleteSupplyDetailAsync(selectedDetail.Supply_Detail_id);

                // update supply total
                var supply = await _service.GetSupplyByIdAsync(selectedDetail.Supply_id);
                if (supply != null)
                {
                    var details = await _service.GetSupplyDetailsBySupplyIdAsync(selectedDetail.Supply_id);
                    supply.Total = details.Sum(sd => sd.Subtotal);
                    await _service.UpdateSupplyAsync(supply);
                }

                // refresh UI
                await LoadInventoryAsync();
                await LoadSuppliesAsync();
                await LoadSupplyDetailsAsync(selectedDetail.Supply_id);

                MessageBox.Show("Item removed from supply");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error removing item: {ex.Message}");
            }
        }
    }
}