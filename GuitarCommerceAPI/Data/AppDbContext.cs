using GuitarCommerceAPI.Models;
using GuitarCommerceAPI.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace GuitarCommerceAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().HasIndex(u => u.Name).IsUnique();

            modelBuilder.Entity<Product>().HasKey(p => p.Id);
        }
    }
}
