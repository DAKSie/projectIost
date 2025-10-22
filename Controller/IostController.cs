using Microsoft.EntityFrameworkCore;

using projectIost.Data;
using projectIost.Models;
using projectIost.Services;

public class IostController
{
    private readonly IostDbContext _db;
    public IostController(IostDbContext db) { _db = db; }

    //Get
    public async Task<List<Item>> GetAllItemsAsync() => await _db.Items.ToListAsync();
    public async Task<List<User>> GetAllUserAsync() => await _db.Users.ToListAsync();

    //Post
    public async Task AddItemAsync(Item i)
    {
        _db.Items.Add(i);
        await _db.SaveChangesAsync();
    }
    public async Task AddUserAsync(User u)
    {
        _db.Users.Add(u);
        await _db.SaveChangesAsync();
    }

    //Put
    public async Task UpdateItemAsync(Item item)
    {
        _db.Items.Update(item);
        await _db.SaveChangesAsync();
    }
    public async Task UpdateUserAsync(User u)
    {
        _db.Users.Update(u);
        await _db.SaveChangesAsync();
    }

    //Delete
    public async Task DeleteItemAsync(int id)
    {
        var item = await _db.Items.FindAsync(id);
        if (item != null) { _db.Items.Remove(item); await _db.SaveChangesAsync(); }
    }
    public async Task DeleteUserAsync(int id)
    {
        var user = await _db.Users.FindAsync(id);
        if (user != null) { _db.Users.Remove(user); await _db.SaveChangesAsync(); }
    }
}
