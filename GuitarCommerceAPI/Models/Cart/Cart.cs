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

        public User User { get; set; } = null!;
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
