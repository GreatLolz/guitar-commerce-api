using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuitarCommerceAPI.Models.Cart
{
    [Table("Carts")]
    public class Cart
    {
        [Required]
        public required string Id { get; set; }

        [Required]
        public required string UserId { get; set; }

        [Required, DefaultValue(true)]
        public required bool IsActive { get; set; }

        public required User User { get; set; }
        public required ICollection<CartItem> Items { get; set; }
    }
}
