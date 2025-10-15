using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuitarCommerceAPI.Models.Order
{
    [Table("Orders")]
    public class Order
    {
        [Required]
        public required string Id { get; set; }

        [Required]
        public required string UserId { get; set; }

        [Required]
        public required ShippingAddress ShippingAddress { get; set; }

        [Required, Precision(18, 2)]
        public required decimal Amount { get; set; }

        [Required, DefaultValue(OrderPaymentStatus.PENDING)]
        public OrderPaymentStatus PaymentStatus { get; set; } = OrderPaymentStatus.PENDING;

        [Required, DefaultValue(OrderDeliveryStatus.UNPAID)]
        public OrderDeliveryStatus DeliveryStatus { get; set; } = OrderDeliveryStatus.PREPARING;

        public string? PaymentIntentId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? PaidAt { get; set; }

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
