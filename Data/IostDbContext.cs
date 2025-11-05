using Microsoft.EntityFrameworkCore;
using projectIost.Models;

namespace projectIost.Data
{
    public class IostDbContext : DbContext
    {
        public IostDbContext(DbContextOptions<IostDbContext> options) : base(options) { }

        public DbSet<Item> Items { get; set; } = null;

        public DbSet<User> Users { get; set; } = null;

        public DbSet<Order> Orders { get; set; } = null;
        public DbSet<Order_Details> OrderDetails { get; set; } = null;

        public DbSet<Supply> Supplies { get; set; } = null;
        public DbSet<Supply_Details> SupplyDetails { get; set; } = null;
        // When adding other database tables use:
        // public DbSet<Order>, DbSet<Supplier>, etc
    }
}