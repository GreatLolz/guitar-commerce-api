using GuitarCommerceAPI.Data;
using GuitarCommerceAPI.Models.Cart;
using Microsoft.EntityFrameworkCore;

namespace GuitarCommerceAPI.Services
{
    public class CartService : ICartService
    {
        private readonly AppDbContext db;

        public CartService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<Cart?> GetActiveCart(string userId)
        {
            return await db.Carts.SingleOrDefaultAsync(x => x.UserId == userId && x.IsActive);
        }

        public async Task<ICollection<Cart>> GetCartHistory(string userId)
        {
            return await db.Carts.Where(x => x.UserId == userId && !x.IsActive).ToListAsync();
        }

        public async Task AddCartItem(string userId, string productId, int quantity)
        {
            Cart? cart = await GetActiveCart(userId);
            if (cart == null)
            {
                Cart newCart = new Cart
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = userId,
                    IsActive = true,
                };

                cart = newCart;
            }

            CartItem newItem = new CartItem
            {
                Id = cart.NextItemId,
                ProductId = productId,
                CartId = cart.Id,
                Quantity = quantity
            };

            cart.NextItemId += 1;
            await db.Carts.AddAsync(cart);
            await db.CartItems.AddAsync(newItem);
            await db.SaveChangesAsync();
        }

        public async Task<bool> DeleteCartItem(string userId, int itemId)
        {
            CartItem? item = await db.CartItems.SingleOrDefaultAsync(x => x.Id == itemId);
            if (item == null)
            {
                return false;
            }

            if (item.Cart.UserId != userId)
            {
                return false;
            }

            db.CartItems.Remove(item);
            await db.SaveChangesAsync();
            return true;
        }
    }
}
