using GuitarCommerceAPI.Models;
using GuitarCommerceAPI.Models.Cart;
using GuitarCommerceAPI.Models.Order;
using GuitarCommerceAPI.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace GuitarCommerceAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().HasIndex(u => u.Name).IsUnique();

            modelBuilder.Entity<Product>().HasKey(p => p.Id);

            modelBuilder.Entity<Cart>().HasKey(c => c.Id);

            modelBuilder.Entity<CartItem>().HasKey(ci => ci.Id);

            modelBuilder.Entity<Order>().HasKey(o => o.Id);

            modelBuilder.Entity<OrderItem>().HasKey(oi => oi.Id);
        }
    }
}
