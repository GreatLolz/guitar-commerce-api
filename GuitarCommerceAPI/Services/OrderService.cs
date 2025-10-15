using GuitarCommerceAPI.Data;
using GuitarCommerceAPI.Models;
using GuitarCommerceAPI.Models.Cart;
using GuitarCommerceAPI.Models.Order;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace GuitarCommerceAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext db;
        private readonly ICartService cartService;
        private readonly IConfiguration configuration;

        public OrderService(AppDbContext db, ICartService cartService, IConfiguration configuration)
        {
            this.db = db;
            this.cartService = cartService;
            this.configuration = configuration;
            StripeConfiguration.ApiKey = this.configuration.GetValue<string>("StripeConfig:SecretKey");
        }

        public async Task<string?> Checkout(string userId, CheckoutData checkoutData)
        {
            Order? order = await createOrder(userId, checkoutData);
            if (order == null)
            {
                return null;
            }

            PaymentIntentService paymentIntentService = new PaymentIntentService();
            PaymentIntent paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
            {
                Amount = (long)(order.Amount * 100),
                Currency = "usd",
                Metadata = new Dictionary<string, string>
                {
                    { "orderId", order.Id },
                    { "userId", userId }
                }
            });

            order.PaymentIntentId = paymentIntent.Id;
            await db.SaveChangesAsync();

            return paymentIntent.ClientSecret;
        }

        public async Task ChangeOrderStatus(string paymentIntentId, OrderStatus status)
        {
            Order? order = await db.Orders.FirstOrDefaultAsync(order => order.PaymentIntentId == paymentIntentId);
            if (order != null)
            {
                order.Status = status;
                await db.SaveChangesAsync();
            }
        }

        private async Task<Order?> createOrder(string userId, CheckoutData checkoutData)
        {
            string orderId = Guid.NewGuid().ToString();
            List<OrderItem> orderItems = new List<OrderItem>();

            Cart? userCart = await cartService.GetActiveCart(userId);
            if (userCart == null)
            {
                return null;
            }

            foreach (CartItem item in userCart.Items)
            {
                OrderItem orderItem = new OrderItem
                {
                    Id = Guid.NewGuid().ToString(),
                    OrderId = orderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price,
                };

                db.OrderItems.Add(orderItem);
                orderItems.Add(orderItem);
            }

            Order order = new Order
            {
                Id = orderId,
                UserId = userId,
                ShippingAddress = new ShippingAddress
                {
                    Email = checkoutData.Email,
                    PhoneNumber = checkoutData.Phone,
                    Name = checkoutData.FullName,
                    AddressLine1 = checkoutData.AddressLine1,
                    AddressLine2 = checkoutData.AddressLine2,
                    City = checkoutData.City,
                    Country = checkoutData.Country,
                    ZipCode = checkoutData.ZipCode,
                },
                Amount = orderItems.Sum(item => item.Quantity * item.UnitPrice),
                Status = OrderStatus.PENDING,
                CreatedAt = DateTime.Now,
            };
            db.Orders.Add(order);
            await db.SaveChangesAsync();

            return order;
        }
    }
}
