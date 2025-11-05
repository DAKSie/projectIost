using Microsoft.EntityFrameworkCore;
using projectIost.Models;

namespace projectIost.Data
{
    public class IostDbContext : DbContext
    {
        public IostDbContext(DbContextOptions<IostDbContext> options) : base(options) { }

        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Order_Details> OrderDetails { get; set; } = null!;
        public DbSet<Supply> Supplies { get; set; } = null!;
        public DbSet<Supply_Details> SupplyDetails { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Explicitly map to table names (backup in case Table attributes don't work)
            modelBuilder.Entity<Item>().ToTable("item");
            modelBuilder.Entity<User>().ToTable("user");
            modelBuilder.Entity<Order>().ToTable("orders");
            modelBuilder.Entity<Order_Details>().ToTable("order_details");
            modelBuilder.Entity<Supply>().ToTable("supply");
            modelBuilder.Entity<Supply_Details>().ToTable("supply_details");
        }
    }
}