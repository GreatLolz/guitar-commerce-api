using GuitarCommerceAPI.Models.Cart;

namespace GuitarCommerceAPI.Services
{
    public interface ICartService
    {
        Task<Cart> GetCart(string userId);
        Task AddCartItem(string cartId, string productId);
        Task DeleteCartItem(string cartId, int itemId);
    }
}
