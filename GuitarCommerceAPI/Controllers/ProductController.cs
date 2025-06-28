using GuitarCommerceAPI.Models.Products;
using GuitarCommerceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GuitarCommerceAPI.Controllers
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> ListProducts()
        {
            try
            {
                List<Product> products = await productService.ListProductsAsync();

                return Ok(new ProductsResponse { Products = products });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while fetching products: " + ex.Message);
                return StatusCode(500, "An error has occurred while fetching products.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] AddProductRequest request)
        {
            try
            {
                Product product = new Product
                {
                    Id = Guid.NewGuid().ToString(),
                    ImageUrl = request.ImageUrl,
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.Price,
                    Category = request.Category,
                    Brand = request.Brand,
                    Stock = request.Stock,
                    Rating = request.Rating,
                    ReviewsCount = request.ReviewsCount
                };

                await productService.AddProductAsync(product);

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while adding product: " + ex.Message);
                return StatusCode(500, "An error has occurred while adding product.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            try
            {
                if (!await productService.DeleteProductAsync(id))
                {
                    return NotFound("Product not found.");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while deleting product: " + ex.Message);
                return StatusCode(500, "An error has occurred while deleting product.");
            }
        }
    }
}
