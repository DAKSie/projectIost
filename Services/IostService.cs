using projectIost.Services;
using Microsoft.EntityFrameworkCore;
using projectIost.Data;
using projectIost.Models;

namespace projectIost.Services
{
    public class IostService : IIostService
    {
        private readonly IostDbContext _context;

        public IostService(IostDbContext context)
        {
            _context = context;
        }

        // ========== ITEM METHODS ==========
        public async Task<List<Item>> GetAllItemsAsync()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<Item?> GetItemByIdAsync(int id)
        {
            return await _context.Items.FindAsync(id);
        }

        public async Task AddItemAsync(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateItemAsync(Item item)
        {
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        // ========== USER METHODS ==========
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User?> AuthenticateUserAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.User_name == username && u.User_password == password);

            return user;
        }

        // ========== ORDER METHODS ==========
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Item)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Item)
                .FirstOrDefaultAsync(o => o.Order_number == id);
        }

        public async Task AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        // ========== ORDER DETAILS METHODS ==========
        public async Task<List<Order_Details>> GetAllOrderDetailsAsync()
        {
            return await _context.OrderDetails
                .Include(od => od.Order)
                .Include(od => od.Item)
                .ToListAsync();
        }

        public async Task<Order_Details?> GetOrderDetailByIdAsync(int id)
        {
            return await _context.OrderDetails
                .Include(od => od.Order)
                .Include(od => od.Item)
                .FirstOrDefaultAsync(od => od.Order_Detail_id == id);
        }

        public async Task<List<Order_Details>> GetOrderDetailsByOrderIdAsync(int orderId)
        {
            return await _context.OrderDetails
                .Include(od => od.Order)
                .Include(od => od.Item)
                .Where(od => od.Order_id == orderId)
                .ToListAsync();
        }

        public async Task AddOrderDetailAsync(Order_Details orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();
        }

        public async Task AddOrderDetailsRangeAsync(List<Order_Details> orderDetails)
        {
            _context.OrderDetails.AddRange(orderDetails);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderDetailAsync(Order_Details orderDetail)
        {
            _context.OrderDetails.Update(orderDetail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderDetailAsync(int id)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail != null)
            {
                _context.OrderDetails.Remove(orderDetail);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteOrderDetailsByOrderIdAsync(int orderId)
        {
            var orderDetails = await _context.OrderDetails
                .Where(od => od.Order_id == orderId)
                .ToListAsync();

            if (orderDetails.Any())
            {
                _context.OrderDetails.RemoveRange(orderDetails);
                await _context.SaveChangesAsync();
            }
        }

        // ========== SUPPLY METHODS ==========
        public async Task<List<Supply>> GetAllSuppliesAsync()
        {
            return await _context.Supplies
                .Include(s => s.User)
                .Include(s => s.SupplyDetails)
                    .ThenInclude(sd => sd.Item)
                .ToListAsync();
        }

        public async Task<Supply?> GetSupplyByIdAsync(int id)
        {
            return await _context.Supplies
                .Include(s => s.User)
                .Include(s => s.SupplyDetails)
                    .ThenInclude(sd => sd.Item)
                .FirstOrDefaultAsync(s => s.Supply_id == id);
        }

        public async Task AddSupplyAsync(Supply supply)
        {
            _context.Supplies.Add(supply);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSupplyAsync(Supply supply)
        {
            _context.Supplies.Update(supply);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSupplyAsync(int id)
        {
            var supply = await _context.Supplies.FindAsync(id);
            if (supply != null)
            {
                _context.Supplies.Remove(supply);
                await _context.SaveChangesAsync();
            }
        }

        // ========== SUPPLY DETAILS METHODS ==========
        public async Task<List<Supply_Details>> GetAllSupplyDetailsAsync()
        {
            return await _context.SupplyDetails
                .Include(sd => sd.Supply)
                .Include(sd => sd.Item)
                .ToListAsync();
        }

        public async Task<Supply_Details?> GetSupplyDetailByIdAsync(int id)
        {
            return await _context.SupplyDetails
                .Include(sd => sd.Supply)
                .Include(sd => sd.Item)
                .FirstOrDefaultAsync(sd => sd.Supply_Detail_id == id);
        }

        public async Task<List<Supply_Details>> GetSupplyDetailsBySupplyIdAsync(int supplyId)
        {
            return await _context.SupplyDetails
                .Include(sd => sd.Supply)
                .Include(sd => sd.Item)
                .Where(sd => sd.Supply_id == supplyId)
                .ToListAsync();
        }

        public async Task AddSupplyDetailAsync(Supply_Details supplyDetail)
        {
            _context.SupplyDetails.Add(supplyDetail);
            await _context.SaveChangesAsync();
        }

        public async Task AddSupplyDetailsRangeAsync(List<Supply_Details> supplyDetails)
        {
            _context.SupplyDetails.AddRange(supplyDetails);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSupplyDetailAsync(Supply_Details supplyDetail)
        {
            _context.SupplyDetails.Update(supplyDetail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSupplyDetailAsync(int id)
        {
            var supplyDetail = await _context.SupplyDetails.FindAsync(id);
            if (supplyDetail != null)
            {
                _context.SupplyDetails.Remove(supplyDetail);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteSupplyDetailsBySupplyIdAsync(int supplyId)
        {
            var supplyDetails = await _context.SupplyDetails
                .Where(sd => sd.Supply_id == supplyId)
                .ToListAsync();

            if (supplyDetails.Any())
            {
                _context.SupplyDetails.RemoveRange(supplyDetails);
                await _context.SaveChangesAsync();
            }
        }
    }
}