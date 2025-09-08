using System.ComponentModel.DataAnnotations.Schema;

namespace GuitarCommerceAPI.Models.Cart
{
    [Table("Carts")]
    public class Cart
    {
        public required string Id { get; set; }
        public required string UserId { get; set; }
        public required bool IsActive { get; set; }

        public required User User { get; set; }
        public required ICollection<CartItem> Items { get; set; }
    }
}
