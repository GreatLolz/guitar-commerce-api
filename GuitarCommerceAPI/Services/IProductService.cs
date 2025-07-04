using GuitarCommerceAPI.Models.Products;

namespace GuitarCommerceAPI.Services
{
    public interface IProductService
    {
        Task<List<Product>> ListProductsAsync(int count = 20, string? filter = null);
        Task<Product?> GetProductByIdAsync(string id);
        Task AddProductAsync(Product product);
        Task<bool> DeleteProductAsync(string id);
    }
}
