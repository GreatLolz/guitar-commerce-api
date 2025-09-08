using GuitarCommerceAPI.Models;
using GuitarCommerceAPI.Models.Cart;
using GuitarCommerceAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GuitarCommerceAPI.Controllers
{
    [Route("api/v1/cart")]
    [ApiController]
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetCart()
        {
            try
            {
                string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                Cart? cart = await cartService.GetActiveCart(userId);
                if (cart == null)
                {
                    return Ok(new GetCartResponse
                    {
                        CartItems = []
                    });
                }

                return Ok(new GetCartResponse
                {
                    CartItems = cart.Items.Select(item => new CartItemDTO
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity
                    }).ToList()
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while fetching cart items: {ex.Message}");
                return StatusCode(500, "An error occured while fetching cart items.");
            }
        }

        [HttpPost()]
        public async Task<IActionResult> AddCartItem([FromBody] AddCartItemRequest request)
        {
            try
            {
                string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                await cartService.AddCartItem(userId, request.ProductId, request.Quantity);

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while adding item to cart: {ex.Message}");
                return StatusCode(500, "An error occured while adding item to cart.");
            }
        }
    }
}
