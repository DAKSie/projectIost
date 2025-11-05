using projectIost.Services;
using Microsoft.EntityFrameworkCore;
using projectIost.Data;
using projectIost.Models;
using System.Security.Policy;

namespace projectIost.Services
{
    public class IostService : IIostService
    {
        private readonly IostDbContext _context;

        public IostService(IostDbContext context)
        {
            _context = context;
        }

        // GET ==================
        //Items
        public async Task<List<Item>> GetAllItemsAsync()
        {
            return await _context.Items.ToListAsync();
        }
        //Users
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
        // GET ==================

        // POST ==================
        // Items
        public async Task AddItemAsync(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
        }
        //Users
        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        // POST ==================

        // PUT ==================
        //Items
        public async Task UpdateItemAsync(Item item)
        {
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
        }
        //Users
        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        // PUT ==================

        // DELETE ==================
        //Items
        public async Task DeleteItemAsync(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
        //Users
        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
        // DELETE ==================

        // LOGIN ==================
        public async Task<User?> AuthenticateUserAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.User_name == username && u.User_password == password);

            return user;
        }
    }
}
