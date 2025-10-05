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

        public async Task<List<Item>> GetAllItemsAsync()
        {
            return await _context.Items.ToListAsync();
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
    }
}
