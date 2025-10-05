using Microsoft.EntityFrameworkCore;

using projectIost.Data;
using projectIost.Models;
using projectIost.Services;

public class IostController
{
    private readonly IostDbContext _db;
    public IostController(IostDbContext db) { _db = db; }

    public async Task<List<Item>> GetAllItemsAsync() => await _db.Items.ToListAsync();

    public async Task AddItemAsync(Item i)
    {
        _db.Items.Add(i);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateItemAsync(Item item)
    {
        _db.Items.Update(item);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteItemAsync(int id)
    {
        var item = await _db.Items.FindAsync(id);
        if (item != null) { _db.Items.Remove(item); await _db.SaveChangesAsync(); }
    }
}
