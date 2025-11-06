using projectIost.Data;
using projectIost.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace projectIost.Services
{
    public class IostService : IIostService
    {
        private readonly IServiceProvider _serviceProvider;

        public IostService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        // REMOVE the CreateNewContext method and using statements
        // Let DI manage the context lifetime

        // ========== ITEM METHODS ==========
        public async Task<List<Item>> GetAllItemsAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            return await context.Items.AsNoTracking().ToListAsync();
        }

        public async Task<Item?> GetItemByIdAsync(int id)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            return await context.Items.AsNoTracking().FirstOrDefaultAsync(i => i.Item_id == id);
        }

        public async Task AddItemAsync(Item item)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            context.Items.Add(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateItemAsync(Item item)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            context.Items.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(int id)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            var item = await context.Items.FindAsync(id);
            if (item != null)
            {
                context.Items.Remove(item);
                await context.SaveChangesAsync();
            }
        }

        // ========== USER METHODS ==========
        public async Task<List<User>> GetAllUsersAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            return await context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            return await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.User_id == id);
        }

        public async Task AddUserAsync(User user)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            context.Users.Update(user);
            await context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            var user = await context.Users.FindAsync(id);
            if (user != null)
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
            }
        }

        public async Task<User?> AuthenticateUserAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            var user = await context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.User_name == username && u.User_password == password);

            return user;
        }

        // ========== ORDER METHODS ==========
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            return await context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Item)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            return await context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Item)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Order_number == id);
        }

        public async Task AddOrderAsync(Order order)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            context.Orders.Add(order);
            await context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            context.Orders.Update(order);
            await context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int id)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            var order = await context.Orders.FindAsync(id);
            if (order != null)
            {
                context.Orders.Remove(order);
                await context.SaveChangesAsync();
            }
        }

        // ========== ORDER DETAILS METHODS ==========
        public async Task<List<Order_Details>> GetAllOrderDetailsAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            return await context.OrderDetails
                .Include(od => od.Order)
                .Include(od => od.Item)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Order_Details?> GetOrderDetailByIdAsync(int id)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            return await context.OrderDetails
                .Include(od => od.Order)
                .Include(od => od.Item)
                .AsNoTracking()
                .FirstOrDefaultAsync(od => od.Order_Detail_id == id);
        }

        public async Task<List<Order_Details>> GetOrderDetailsByOrderIdAsync(int orderId)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            return await context.OrderDetails
                .Include(od => od.Order)
                .Include(od => od.Item)
                .AsNoTracking()
                .Where(od => od.Order_id == orderId)
                .ToListAsync();
        }

        public async Task AddOrderDetailAsync(Order_Details orderDetail)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            context.OrderDetails.Add(orderDetail);
            await context.SaveChangesAsync();
        }

        public async Task AddOrderDetailsRangeAsync(List<Order_Details> orderDetails)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            context.OrderDetails.AddRange(orderDetails);
            await context.SaveChangesAsync();
        }

        public async Task UpdateOrderDetailAsync(Order_Details orderDetail)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            context.OrderDetails.Update(orderDetail);
            await context.SaveChangesAsync();
        }

        public async Task DeleteOrderDetailAsync(int id)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            var orderDetail = await context.OrderDetails.FindAsync(id);
            if (orderDetail != null)
            {
                context.OrderDetails.Remove(orderDetail);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteOrderDetailsByOrderIdAsync(int orderId)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            var orderDetails = await context.OrderDetails
                .Where(od => od.Order_id == orderId)
                .ToListAsync();

            if (orderDetails.Any())
            {
                context.OrderDetails.RemoveRange(orderDetails);
                await context.SaveChangesAsync();
            }
        }

        // ========== SUPPLY METHODS ==========
        public async Task<List<Supply>> GetAllSuppliesAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            return await context.Supplies
                .Include(s => s.User)
                .Include(s => s.SupplyDetails)
                    .ThenInclude(sd => sd.Item)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Supply?> GetSupplyByIdAsync(int id)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            return await context.Supplies
                .Include(s => s.User)
                .Include(s => s.SupplyDetails)
                    .ThenInclude(sd => sd.Item)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Supply_id == id);
        }

        public async Task AddSupplyAsync(Supply supply)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            context.Supplies.Add(supply);
            await context.SaveChangesAsync();
        }

        public async Task UpdateSupplyAsync(Supply supply)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            context.Supplies.Update(supply);
            await context.SaveChangesAsync();
        }

        public async Task DeleteSupplyAsync(int id)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            var supply = await context.Supplies.FindAsync(id);
            if (supply != null)
            {
                context.Supplies.Remove(supply);
                await context.SaveChangesAsync();
            }
        }

        // ========== SUPPLY DETAILS METHODS ==========
        public async Task<List<Supply_Details>> GetAllSupplyDetailsAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            return await context.SupplyDetails
                .Include(sd => sd.Supply)
                .Include(sd => sd.Item)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Supply_Details?> GetSupplyDetailByIdAsync(int id)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            return await context.SupplyDetails
                .Include(sd => sd.Supply)
                .Include(sd => sd.Item)
                .AsNoTracking()
                .FirstOrDefaultAsync(sd => sd.Supply_Detail_id == id);
        }

        public async Task<List<Supply_Details>> GetSupplyDetailsBySupplyIdAsync(int supplyId)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            return await context.SupplyDetails
                .Include(sd => sd.Supply)
                .Include(sd => sd.Item)
                .AsNoTracking()
                .Where(sd => sd.Supply_id == supplyId)
                .ToListAsync();
        }

        public async Task AddSupplyDetailAsync(Supply_Details supplyDetail)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            context.SupplyDetails.Add(supplyDetail);
            await context.SaveChangesAsync();
        }

        public async Task AddSupplyDetailsRangeAsync(List<Supply_Details> supplyDetails)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            context.SupplyDetails.AddRange(supplyDetails);
            await context.SaveChangesAsync();
        }

        public async Task UpdateSupplyDetailAsync(Supply_Details supplyDetail)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            context.SupplyDetails.Update(supplyDetail);
            await context.SaveChangesAsync();
        }

        public async Task DeleteSupplyDetailAsync(int id)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            var supplyDetail = await context.SupplyDetails.FindAsync(id);
            if (supplyDetail != null)
            {
                context.SupplyDetails.Remove(supplyDetail);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteSupplyDetailsBySupplyIdAsync(int supplyId)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IostDbContext>();
            var supplyDetails = await context.SupplyDetails
                .Where(sd => sd.Supply_id == supplyId)
                .ToListAsync();

            if (supplyDetails.Any())
            {
                context.SupplyDetails.RemoveRange(supplyDetails);
                await context.SaveChangesAsync();
            }
        }
    }
}