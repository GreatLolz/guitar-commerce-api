using GuitarCommerceAPI.Models.Order;
using GuitarCommerceAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Security.Claims;

namespace GuitarCommerceAPI.Controllers
{
    [Route("api/v1/orders")]
    [ApiController]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IConfiguration configuration;

        public OrderController(IOrderService orderService, IConfiguration configuration)
        {
            this.orderService = orderService;
            this.configuration = configuration;
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutData request)
        {
            try
            {
                string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                string? clientSecret = await orderService.Checkout(userId, request);
                if (string.IsNullOrEmpty(clientSecret))
                {
                    throw new Exception($"Failed to create order for user {userId}");
                }

                return Ok(new CheckoutResponse { ClientSecret = clientSecret });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while checking out: {ex.Message}");
                return StatusCode(500, "An error occured while checking out.");
            }
        }

        [HttpPost("webhook")]
        [AllowAnonymous]
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var signature = Request.Headers["Stripe-Signature"];
            string? endpointSecret = configuration.GetValue<string>("StripeConfig:WebhookSecret");

            try
            {
                Event stripeEvent = EventUtility.ConstructEvent(json, signature, endpointSecret);
                PaymentIntent? intent = stripeEvent.Data.Object as PaymentIntent;
                if (intent == null)
                {
                    throw new Exception("Could not find payment intent.");
                }

                switch (stripeEvent.Type)
                {
                    case EventTypes.PaymentIntentSucceeded:
                        await orderService.ChangeOrderPaymentStatus(intent.Id, OrderPaymentStatus.COMPLETED);
                        break;
                    case EventTypes.PaymentIntentPaymentFailed:
                        await orderService.ChangeOrderPaymentStatus(intent.Id, OrderPaymentStatus.FAILED);
                        break;
                }

                return Ok();
            }
            catch (StripeException ex)
            {
                Console.WriteLine($"Webhook error: {ex.Message}");
                return BadRequest($"Webhook error: {ex.Message}");
            }
        }

    }
}
