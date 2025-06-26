using GuitarCommerceAPI.Data;
using GuitarCommerceAPI.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace GuitarCommerceAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext db;

        public ProductService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Product>?> ListProductsAsync()
        {
            try 
            {                 
                return await db.Products.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while fetching products from the database: " + ex.Message);
                return null;
            }
        }

        public async Task<Product?> GetProductByIdAsync(string id)
        {
            try
            {
                return await db.Products.SingleOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while fetching product by ID from the database: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> AddProductAsync(Product product)
        {
            try
            {
                await db.Products.AddAsync(product);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while adding product to the database: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            try
            {
                Product? product = db.Products.SingleOrDefault(p => p.Id == id);
                if (product == null)
                {
                    throw new Exception("Attempted to delete non-existent product.");
                }
                db.Products.Remove(product);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while deleting product from the database: " + ex.Message);
                return false;
            }
        }
    }
}
