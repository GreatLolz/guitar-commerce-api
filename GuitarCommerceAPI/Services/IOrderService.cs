using GuitarCommerceAPI.Models.Order;

namespace GuitarCommerceAPI.Services
{
    public interface IOrderService
    {
        Task<string?> Checkout(string userId, CheckoutData checkoutData);
        Task ChangeOrderStatus(string paymentIntentId, OrderStatus status);
    }
}
