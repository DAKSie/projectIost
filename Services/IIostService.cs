using projectIost.Data;
using projectIost.Models;

namespace projectIost.Services
{
    public interface IIostService
    {
        // ========== ITEM METHODS ==========
        Task<List<Item>> GetAllItemsAsync();
        Task<Item?> GetItemByIdAsync(int id);
        Task AddItemAsync(Item item);
        Task UpdateItemAsync(Item item);
        Task DeleteItemAsync(int id);

        // ========== USER METHODS ==========
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task<User?> AuthenticateUserAsync(string username, string password);

        // ========== ORDER METHODS ==========
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(int id);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int id);

        // ========== ORDER DETAILS METHODS ==========
        Task<List<Order_Details>> GetAllOrderDetailsAsync();
        Task<Order_Details?> GetOrderDetailByIdAsync(int id);
        Task<List<Order_Details>> GetOrderDetailsByOrderIdAsync(int orderId);
        Task AddOrderDetailAsync(Order_Details orderDetail);
        Task AddOrderDetailsRangeAsync(List<Order_Details> orderDetails);
        Task UpdateOrderDetailAsync(Order_Details orderDetail);
        Task DeleteOrderDetailAsync(int id);
        Task DeleteOrderDetailsByOrderIdAsync(int orderId);

        // ========== SUPPLY METHODS ==========
        Task<List<Supply>> GetAllSuppliesAsync();
        Task<Supply?> GetSupplyByIdAsync(int id);
        Task AddSupplyAsync(Supply supply);
        Task UpdateSupplyAsync(Supply supply);
        Task DeleteSupplyAsync(int id);

        // ========== SUPPLY DETAILS METHODS ==========
        Task<List<Supply_Details>> GetAllSupplyDetailsAsync();
        Task<Supply_Details?> GetSupplyDetailByIdAsync(int id);
        Task<List<Supply_Details>> GetSupplyDetailsBySupplyIdAsync(int supplyId);
        Task AddSupplyDetailAsync(Supply_Details supplyDetail);
        Task AddSupplyDetailsRangeAsync(List<Supply_Details> supplyDetails);
        Task UpdateSupplyDetailAsync(Supply_Details supplyDetail);
        Task DeleteSupplyDetailAsync(int id);
        Task DeleteSupplyDetailsBySupplyIdAsync(int supplyId);
    }
}