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
            modelBuilder.Entity<User>().Property(u => u.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<User>().Property(u => u.PasswordHash).IsRequired().HasMaxLength(256);

            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Product>().Property(p => p.ImageUrl).IsRequired().HasMaxLength(2048);
            modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Product>().Property(p => p.Description).IsRequired().HasMaxLength(1000);
            modelBuilder.Entity<Product>().Property(p => p.Price).IsRequired().HasPrecision(18, 2);
            modelBuilder.Entity<Product>().Property(p => p.Category).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Product>().Property(p => p.Brand).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Product>().Property(p => p.Stock).IsRequired().HasDefaultValue(0);
            modelBuilder.Entity<Product>().Property(p => p.Rating).IsRequired().HasPrecision(3, 2).HasDefaultValue(0);
            modelBuilder.Entity<Product>().Property(p => p.ReviewsCount).IsRequired().HasDefaultValue(0);
        }
    }
}
