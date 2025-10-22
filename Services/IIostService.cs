using projectIost.Data;
using projectIost.Models;

namespace projectIost.Services
{
    public interface IIostService
    {
        Task<List<Item>> GetAllItemsAsync();
        Task<List<User>> GetAllUsersAsync();

        Task AddItemAsync(Item item);
        Task AddUserAsync(User user);

        Task UpdateItemAsync(Item item);
        Task UpdateUserAsync(User user);

        Task DeleteItemAsync(int id);
        Task DeleteUserAsync(int id);
    }
}
