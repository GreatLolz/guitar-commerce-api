using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuitarCommerceAPI.Models.Order
{
    [Table("OrderItems")]
    public class OrderItem
    {
        [Required]
        public required string Id { get; set; }

        [Required]
        public required string OrderId { get; set; }

        [Required]
        public required string ProductId { get; set; }

        [Required]
        public required int Quantity { get; set; }

        [Required, Precision(18, 2)]
        public required decimal UnitPrice { get; set; }

        public Order Order { get; set; } = null!;
    }
}
