using GuitarCommerceAPI.Data;
using GuitarCommerceAPI.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace GuitarCommerceAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext db;
        private readonly List<string> productCategories = new List<string>
        {
            "guitars",
            "basses",
            "drums",
            "amps-effects",
            "keys-midi",
            "recording",
            "accessories"
        };

        public ProductService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Product>> ListProductsAsync(int count = 20, string? category = null)
        {
            if (category != null && !productCategories.Contains(category.ToLowerInvariant()))
            {
                Console.WriteLine("Invalid category has been provided while fetching products. Defaulting to any.");
                category = null;
            }


            return await db.Products
                .Where(p => category == null || p.Category == category)
                .Take(count)
                .ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(string id)
        {

            return await db.Products.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddProductAsync(Product product)
        {
            await db.Products.AddAsync(product);
            await db.SaveChangesAsync();
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            Product? product = db.Products.SingleOrDefault(p => p.Id == id);
            if (product == null)
            {
                return false;
            }

            db.Products.Remove(product);
            await db.SaveChangesAsync();

            return true;
        }
    }
}
