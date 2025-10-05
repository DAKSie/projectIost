using projectIost.Data;
using projectIost.Models;

namespace projectIost.Services
{
    public interface IIostService
    {
        Task<List<Item>> GetAllItemsAsync();
        Task AddItemAsync(Item item);
        Task UpdateItemAsync(Item item);
        Task DeleteItemAsync(int id);
        // Add other methods as needed
    }
}
