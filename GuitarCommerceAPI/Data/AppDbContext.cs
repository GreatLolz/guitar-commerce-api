using GuitarCommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GuitarCommerceAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().HasIndex(u => u.Name).IsUnique();
            modelBuilder.Entity<User>().Property(u => u.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<User>().Property(u => u.PasswordHash).IsRequired().HasMaxLength(256);
        }
    }
}
