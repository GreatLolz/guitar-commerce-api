using GuitarCommerceAPI.Models.Cart;

namespace GuitarCommerceAPI.Services
{
    public interface ICartService
    {
        Task<Cart?> GetActiveCart(string userId);
        Task<ICollection<Cart>> GetCartHistory(string userId);
        Task AddCartItem(string userId, string productId, int quantity);
        Task<bool> DeleteCartItem(string userId, int itemId);
    }
}
