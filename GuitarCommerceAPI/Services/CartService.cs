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
            return await db.Carts
                .Include(x => x.User)
                .Include(x => x.Items)
                .SingleOrDefaultAsync(x => x.UserId == userId && x.IsActive);
        }

        public async Task<ICollection<Cart>> GetCartHistory(string userId)
        {
            return await db.Carts.Where(x => x.UserId == userId && !x.IsActive)
                .Include(x => x.User)
                .Include(x => x.Items)
                .ToListAsync();
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
                await db.Carts.AddAsync(cart);
            }

            CartItem newItem = new CartItem
            {
                Id = Guid.NewGuid().ToString(),
                ProductId = productId,
                CartId = cart.Id,
                Quantity = quantity
            };

            await db.CartItems.AddAsync(newItem);
            await db.SaveChangesAsync();
        }

        public async Task<bool> DeleteCartItem(string userId, string itemId)
        {
            CartItem? item = await db.CartItems
                .Include(x => x.Cart)
                .SingleOrDefaultAsync(x => x.Id == itemId);
            if (item == null)
            {
                return false;
            }

            Cart cart = item.Cart;

            if (cart.UserId != userId)
            {
                return false;
            }

            db.CartItems.Remove(item);
            await db.SaveChangesAsync();

            if (cart.Items.Count == 0)
            {
                db.Carts.Remove(cart);
                await db.SaveChangesAsync();
            }

            return true;
        }
    }
}
