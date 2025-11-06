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
    public partial class OrderView : Form
    {
        private readonly IIostService? _service;
        private readonly BindingSource _inventoryBindingSource = new();
        private readonly BindingSource _orderMasterBindingSource = new();
        private readonly BindingSource _orderDetailBindingSource = new();

        private InventoryView _inventoryView;
        private SupplyView _supplyView;
        private AnalyticsView _analyticsView;
        private LoginView _loginView;

        public OrderView()
        {
            InitializeComponent();
            SetupDGV();
        }

        public OrderView(IIostService service) : this()
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        private void SetupDGV()
        {
            dgvInventory.DataSource = _inventoryBindingSource;
            dgvInventory.AllowUserToAddRows = false;

            dgvOrderMaster.DataSource = _orderMasterBindingSource;
            dgvOrderMaster.AllowUserToAddRows = false;
            dgvOrderMaster.SelectionChanged += DgvOrderMaster_SelectionChanged;

            dgvOrderDetails.DataSource = _orderDetailBindingSource;
            dgvOrderDetails.AllowUserToAddRows = false;
        }

        private async Task LoadInventoryAsync()
        {
            if (_service == null) return;

            try
            {
                var items = await _service.GetAllItemsAsync();

                // manual binding for control
                dgvInventory.Invoke((MethodInvoker)delegate
                {
                    dgvInventory.AutoGenerateColumns = false;
                    dgvInventory.Columns.Clear();

                    // manually added columns
                    dgvInventory.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "Item_id",
                        HeaderText = "ID",
                        Width = 50
                    });
                    dgvInventory.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "Item_SKU",
                        HeaderText = "SKU",
                        Width = 80
                    });
                    dgvInventory.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "Item_name",
                        HeaderText = "Name",
                        Width = 150
                    });
                    dgvInventory.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "Item_quantity",
                        HeaderText = "Quantity",
                        Width = 70
                    });
                    dgvInventory.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "Item_cost",
                        HeaderText = "Cost",
                        Width = 70
                    });
                    dgvInventory.Columns.Add(new DataGridViewTextBoxColumn
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

        private async Task LoadOrdersAsync()
        {
            if (_service == null) return;

            try
            {
                var orders = await _service.GetAllOrdersAsync();

                dgvOrderMaster.Invoke((MethodInvoker)delegate
                {
                    dgvOrderMaster.AutoGenerateColumns = false;
                    dgvOrderMaster.Columns.Clear();

                    // update DataPropertyName to match new property name
                    dgvOrderMaster.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "Order_id",  
                        HeaderText = "Order ID",
                        Width = 70
                    });
                    dgvOrderMaster.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "Date",
                        HeaderText = "Date",
                        Width = 100
                    });
                    dgvOrderMaster.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "Customer",
                        HeaderText = "Customer",
                        Width = 150
                    });
                    dgvOrderMaster.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "Total",
                        HeaderText = "Total",
                        Width = 80
                    });

                    _orderMasterBindingSource.DataSource = new BindingList<Order>(orders);
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load orders: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadOrderDetailsAsync(int orderId)
        {
            if (_service == null) return;

            try
            {
                var orderDetails = await _service.GetOrderDetailsByOrderIdAsync(orderId);

                dgvOrderDetails.Invoke((MethodInvoker)delegate
                {
                    dgvOrderDetails.AutoGenerateColumns = false;
                    dgvOrderDetails.Columns.Clear();

                    // manually added columns
                    dgvOrderDetails.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "Order_Detail_id",
                        HeaderText = "Detail ID",
                        Width = 70
                    });
                    dgvOrderDetails.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "Order_id",
                        HeaderText = "Order ID",
                        Width = 70
                    });
                    dgvOrderDetails.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "Item_id",
                        HeaderText = "Item ID",
                        Width = 70
                    });
                    dgvOrderDetails.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "Quantity",
                        HeaderText = "Quantity",
                        Width = 70
                    });
                    dgvOrderDetails.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "Price",
                        HeaderText = "Price",
                        Width = 70
                    });
                    dgvOrderDetails.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "Subtotal",
                        HeaderText = "Subtotal",
                        Width = 70
                    });

                    _orderDetailBindingSource.DataSource = new BindingList<Order_Details>(orderDetails);
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load order details: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void DgvOrderMaster_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOrderMaster.CurrentRow?.DataBoundItem is Order selectedOrder)
            {
                // fill text text fields with order info
                txtOrderID.Text = selectedOrder.Order_id.ToString();
                txtOrderDate.Text = selectedOrder.Date.ToString("yyyy-MM-dd");
                txtCustomerName.Text = selectedOrder.Customer;

                // load order details for selected order
                await LoadOrderDetailsAsync(selectedOrder.Order_id);
            }
        }

        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);
            await LoadAllDataAsync();
        }


        private async Task LoadAllDataAsync()
        {
            try
            {
                // load sequentially, not concurrently
                await LoadInventoryAsync();
                await LoadOrdersAsync(); 

                if (_orderMasterBindingSource.Count > 0)
                {
                    var first = _orderMasterBindingSource[0] as Order; 
                    if (first != null)
                    {
                        await LoadOrderDetailsAsync(first.Order_id); 
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

        private void btnSupply_Click(object sender, EventArgs e)
        {
            if (_supplyView == null || _supplyView.IsDisposed)
            {
                _supplyView = Program.ServiceProvider.GetRequiredService<SupplyView>();
            }
            this.Hide();
            _supplyView.Show();
            _supplyView.BringToFront();
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

        private async void btnAddOrder_Click(object sender, EventArgs e)
        {
            if (_service == null)
            {
                MessageBox.Show("Service not available", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // validate input
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text) || string.IsNullOrWhiteSpace(txtOrderDate.Text))
            {
                MessageBox.Show("Please enter Customer Name and Date", "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // parse date
                if (!DateTime.TryParse(txtOrderDate.Text, out DateTime orderDate))
                {
                    MessageBox.Show("Please enter a valid date format (YYYY-MM-DD)", "Validation Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // create new order
                var newOrder = new Order
                {
                    Date = orderDate,
                    Customer = txtCustomerName.Text.Trim(),
                    Total = 0, // calculated when items are added
                };

                await _service.AddOrderAsync(newOrder);

                MessageBox.Show("Order created successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                // refresh orders list
                await LoadOrdersAsync();

                // clear fields
                btnClearOrder_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to add order: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEditOrder_Click(object sender, EventArgs e)
        {
            if (_service == null || string.IsNullOrWhiteSpace(txtOrderID.Text))
            {
                MessageBox.Show("Please select an order to edit", "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // get selected order ID
                if (!int.TryParse(txtOrderID.Text, out int orderId))
                {
                    MessageBox.Show("Invalid Order ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // validate input
                if (string.IsNullOrWhiteSpace(txtCustomerName.Text) || string.IsNullOrWhiteSpace(txtOrderDate.Text))
                {
                    MessageBox.Show("Please enter Customer Name and Date", "Validation Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // parse date
                if (!DateTime.TryParse(txtOrderDate.Text, out DateTime orderDate))
                {
                    MessageBox.Show("Please enter a valid date format (YYYY-MM-DD)", "Validation Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // get existing order
                var existingOrder = await _service.GetOrderByIdAsync(orderId);
                if (existingOrder == null)
                {
                    MessageBox.Show("Order not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // update order info
                existingOrder.Date = orderDate;
                existingOrder.Customer = txtCustomerName.Text.Trim();

                await _service.UpdateOrderAsync(existingOrder);

                MessageBox.Show("Order updated successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                // refresh orders list
                await LoadOrdersAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to update order: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearOrder_Click(object sender, EventArgs e)
        {
            // clear form
            txtOrderID.Clear();
            txtOrderDate.Clear();
            txtCustomerName.Clear();
            txtQuantityOrder.Clear(); 

            _orderDetailBindingSource.DataSource = new BindingList<Order_Details>();
        }

        private async void btnDelOrder_Click(object sender, EventArgs e)
        {
            if (_service == null || string.IsNullOrWhiteSpace(txtOrderID.Text))
            {
                MessageBox.Show("Please select an order to delete", "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // get selected order ID
                if (!int.TryParse(txtOrderID.Text, out int orderId))
                {
                    MessageBox.Show("Invalid Order ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // confirm delete
                var result = MessageBox.Show(
                    $"Are you sure you want to delete order #{orderId}? This will also delete all order details.",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // delete order details
                    await _service.DeleteOrderDetailsByOrderIdAsync(orderId);

                    // then delete order
                    await _service.DeleteOrderAsync(orderId);

                    MessageBox.Show("Order deleted successfully!", "Success",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // refresh orders list and clear fields
                    await LoadOrdersAsync();
                    btnClearOrder_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to delete order: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnAddItem_Click(object sender, EventArgs e)
        {
            if (_service == null) return;

            // input validation
            if (!int.TryParse(txtOrderID.Text, out int orderId) ||
                dgvInventory.CurrentRow?.DataBoundItem is not Item selectedItem ||
                !int.TryParse(txtQuantityOrder.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Please select an order, item, and enter valid quantity", "Validation Error");
                return;
            }

            try
            {
                // check stock
                if (quantity > selectedItem.Item_quantity)
                {
                    MessageBox.Show($"Only {selectedItem.Item_quantity} available");
                    return;
                }

                // add order detail
                var orderDetail = new Order_Details
                {
                    Order_id = orderId,
                    Item_id = selectedItem.Item_id,
                    Quantity = quantity,
                    Price = selectedItem.Item_price,
                    Subtotal = selectedItem.Item_price * quantity
                };

                await _service.AddOrderDetailAsync(orderDetail);

                // update inventory
                selectedItem.Item_quantity -= quantity;
                await _service.UpdateItemAsync(selectedItem);

                // update order total
                var order = await _service.GetOrderByIdAsync(orderId);
                if (order != null)
                {
                    var details = await _service.GetOrderDetailsByOrderIdAsync(orderId);
                    order.Total = details.Sum(od => od.Subtotal);
                    await _service.UpdateOrderAsync(order);
                }

                // refresh UI
                await LoadInventoryAsync();
                await LoadOrdersAsync();
                await LoadOrderDetailsAsync(orderId);

                txtQuantityOrder.Clear();
                MessageBox.Show($"Added {quantity} {selectedItem.Item_name} to order");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async Task UpdateOrderTotal(int orderId)
        {
            var order = await _service.GetOrderByIdAsync(orderId);
            if (order != null)
            {
                var details = await _service.GetOrderDetailsByOrderIdAsync(orderId);
                order.Total = details.Sum(od => od.Subtotal);
                await _service.UpdateOrderAsync(order);
            }
        }

        private async Task RefreshData(int orderId)
        {
            await LoadInventoryAsync();
            await LoadOrdersAsync();
            await LoadOrderDetailsAsync(orderId);
        }

        private async void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (_service == null) return;

            if (dgvOrderDetails.CurrentRow?.DataBoundItem is not Order_Details selectedDetail)
            {
                MessageBox.Show("Please select an item to remove from the order");
                return;
            }

            try
            {
                // get item to restore inventory
                var item = await _service.GetItemByIdAsync(selectedDetail.Item_id);
                if (item != null)
                {
                    // add quantity back to inventory
                    item.Item_quantity += selectedDetail.Quantity;
                    await _service.UpdateItemAsync(item);
                }

                // remove order detail
                await _service.DeleteOrderDetailAsync(selectedDetail.Order_Detail_id);

                // update order total
                var order = await _service.GetOrderByIdAsync(selectedDetail.Order_id);
                if (order != null)
                {
                    var details = await _service.GetOrderDetailsByOrderIdAsync(selectedDetail.Order_id);
                    order.Total = details.Sum(od => od.Subtotal);
                    await _service.UpdateOrderAsync(order);
                }

                // refresh UI
                await LoadInventoryAsync();
                await LoadOrdersAsync();
                await LoadOrderDetailsAsync(selectedDetail.Order_id);

                MessageBox.Show("Item removed from order");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error removing item: {ex.Message}");
            }
        }
    }
}