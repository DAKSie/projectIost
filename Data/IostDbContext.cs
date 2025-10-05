using Microsoft.EntityFrameworkCore;
using projectIost.Models;

namespace projectIost.Data
{
    public class IostDbContext : DbContext
    {
        public IostDbContext(DbContextOptions<IostDbContext> options) : base(options) { }

        public DbSet<Item> Items { get; set; } = null;
        // When adding other database tables use:
        // public DbSet<Order>, DbSet<Supplier>, etc
    }
}