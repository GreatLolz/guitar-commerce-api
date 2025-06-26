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
            List<Product>? products = await productService.ListProductsAsync();
            if (products == null)
            {
                return StatusCode(500, "An error has occurred while fetching products from the database.");
            }

            return Ok(new ProductsResponse { Products = products });
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] AddProductRequest request)
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

            if (!await productService.AddProductAsync(product))
            {
                return StatusCode(500, "An error has occurred while adding the product to the database.");
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            if (!await productService.DeleteProductAsync(id))
            {
                return StatusCode(500, "An error has occurred while deleting the product from the database.");
            }
            return Ok();
        }
    }
}
