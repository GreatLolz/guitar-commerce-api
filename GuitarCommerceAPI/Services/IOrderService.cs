using GuitarCommerceAPI.Models.Order;

namespace GuitarCommerceAPI.Services
{
    public interface IOrderService
    {
        Task<string?> Checkout(string userId, CheckoutData checkoutData);
        Task ChangeOrderPaymentStatus(string paymentIntentId, OrderPaymentStatus status);
        Task ChangeOrderDeliveryStatus(string orderId, OrderDeliveryStatus status);
    }
}
