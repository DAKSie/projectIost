using Microsoft.Extensions.DependencyInjection;
using projectIost.Models;
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
        private readonly IIostService _service;

        // Helper class for monthly sales data
        private class MonthlySalesData
        {
            public DateTime Month { get; set; }
            public decimal TotalSales { get; set; }
        }

        // Helper class for best seller data
        private class BestSellerData
        {
            public int ItemId { get; set; }
            public int TotalQuantity { get; set; }
            public decimal TotalRevenue { get; set; }
        }

        public AnalyticsView(IIostService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            InitializeComponent();
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

        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);
            await LoadAnalyticsDataAsync();
        }

        private async Task LoadAnalyticsDataAsync()
        {
            if (_service == null) return;

            try
            {
                // Show loading state
                label8.Text = "Loading...";
                label10.Text = "Loading...";
                label11.Text = "Loading...";
                dgvSales.Rows.Clear();
                dgvSales.Rows.Add("Loading data...", "Please wait");

                // Load all necessary data
                var orders = await _service.GetAllOrdersAsync();
                var supplies = await _service.GetAllSuppliesAsync();
                var items = await _service.GetAllItemsAsync();
                var orderDetails = await _service.GetAllOrderDetailsAsync();

                // Update analytics displays
                UpdateSalesOverview(orders, supplies);
                UpdateBestSellers(orderDetails, items);
                UpdateMonthlySalesChart(orders);

                // Optional: Update inventory metrics
                CalculateInventoryMetrics(items);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load analytics data: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Set error state
                label8.Text = "Error";
                label10.Text = "Error";
                label11.Text = "Error";
                dgvSales.Rows.Clear();
                dgvSales.Rows.Add("Error loading data", "Please try again");
            }
        }

        private void UpdateSalesOverview(List<Order> orders, List<Supply> supplies)
        {
            try
            {
                // Calculate totals
                decimal totalSales = orders.Sum(o => o.Total);
                decimal totalSpending = supplies.Sum(s => s.Total);
                decimal profit = totalSales - totalSpending;

                // Update labels with proper formatting
                label8.Text = profit.ToString("C2"); // Profit
                label10.Text = totalSales.ToString("C2"); // Total Sales

                // Update label for total spending
                label11.Text = totalSpending.ToString("C2"); // Total Spending
                label6.Text = "Total Spending"; // Update the label text
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error calculating sales overview: {ex.Message}");
                label8.Text = "Error";
                label10.Text = "Error";
                label11.Text = "Error";
            }
        }

        private void UpdateBestSellers(List<Order_Details> orderDetails, List<Item> items)
        {
            try
            {
                // Group order details by item and calculate total quantity sold
                var bestSellers = orderDetails
                    .GroupBy(od => od.Item_id)
                    .Select(g => new BestSellerData
                    {
                        ItemId = g.Key,
                        TotalQuantity = g.Sum(od => od.Quantity),
                        TotalRevenue = g.Sum(od => od.Subtotal)
                    })
                    .OrderByDescending(x => x.TotalQuantity)
                    .Take(5) // Top 5 best sellers
                    .ToList();

                // Clear existing data in the sales grid
                dgvSales.Rows.Clear();

                // Populate the grid with best sellers
                foreach (var bestSeller in bestSellers)
                {
                    var item = items.FirstOrDefault(i => i.Item_id == bestSeller.ItemId);
                    if (item != null)
                    {
                        dgvSales.Rows.Add(
                            item.Item_name,
                            $"{bestSeller.TotalQuantity} sold (${bestSeller.TotalRevenue:F2})"
                        );
                    }
                }

                // If no best sellers found, show message
                if (!bestSellers.Any())
                {
                    dgvSales.Rows.Add("No sales data", "N/A");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error calculating best sellers: {ex.Message}");
                dgvSales.Rows.Clear();
                dgvSales.Rows.Add("Error loading data", "Please try again");
            }
        }

        private void UpdateMonthlySalesChart(List<Order> orders)
        {
            try
            {
                // Group orders by month and calculate monthly sales
                var monthlySales = orders
                    .GroupBy(o => new { o.Date.Year, o.Date.Month })
                    .Select(g => new MonthlySalesData
                    {
                        Month = new DateTime(g.Key.Year, g.Key.Month, 1),
                        TotalSales = g.Sum(o => o.Total)
                    })
                    .OrderBy(x => x.Month)
                    .ToList();

                // Update the Guna chart with monthly data
                UpdateChartWithMonthlyData(monthlySales);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating monthly sales chart: {ex.Message}");
            }
        }

        private void UpdateChartWithMonthlyData(List<MonthlySalesData> monthlyData)
        {
            try
            {
                // This is where you would update your Guna chart
                // Since I don't know your exact Guna chart setup, here's a basic structure:

                if (cAnalytics != null && monthlyData.Any())
                {
                    // Clear existing data
                    // cAnalytics.Datasets.Clear();

                    // Create new dataset
                    // var dataset = new Guna.Charts.WinForms.GunaLineDataset();
                    // dataset.Label = "Monthly Sales";

                    foreach (var data in monthlyData)
                    {
                        string monthName = data.Month.ToString("MMM yyyy");
                        double salesValue = (double)data.TotalSales;

                        // Add to dataset
                        // dataset.DataPoints.Add(monthName, salesValue);

                        // For debugging, output to console
                        Console.WriteLine($"{monthName}: ${salesValue:F2}");
                    }

                    // Add dataset to chart and update
                    // cAnalytics.Datasets.Add(dataset);
                    // cAnalytics.Update();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating chart: {ex.Message}");
            }
        }

        private void CalculateInventoryMetrics(List<Item> items)
        {
            try
            {
                // Calculate inventory value
                decimal totalInventoryValue = items.Sum(i => i.Item_quantity * i.Item_cost);
                decimal totalPotentialRevenue = items.Sum(i => i.Item_quantity * i.Item_price);

                // Find low stock items
                var lowStockItems = items.Where(i => i.Item_quantity < 10).Count();
                var outOfStockItems = items.Where(i => i.Item_quantity == 0).Count();

                // You can display these metrics in additional labels or console
                Console.WriteLine($"Inventory Value: {totalInventoryValue:C2}");
                Console.WriteLine($"Potential Revenue: {totalPotentialRevenue:C2}");
                Console.WriteLine($"Low Stock Items: {lowStockItems}");
                Console.WriteLine($"Out of Stock Items: {outOfStockItems}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calculating inventory metrics: {ex.Message}");
            }
        }

        // Refresh button handler - you can add a refresh button to your form
        private async void btnRefreshAnalytics_Click(object sender, EventArgs e)
        {
            await LoadAnalyticsDataAsync();
        }

        // Method to refresh analytics from other forms if needed
        public async void RefreshAnalyticsData()
        {
            await LoadAnalyticsDataAsync();
        }
    }
}