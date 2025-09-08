using GuitarCommerceAPI.Models.Products;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuitarCommerceAPI.Models.Cart
{
    [Table("CartItems")]
    public class CartItem
    {
        [Required]
        public required int Id { get; set; }

        [Required]
        public required string CartId { get; set; }

        [Required]
        public required string ProductId { get; set; }

        [Required]
        public required int Quantity { get; set; }

        public required Cart Cart { get; set; }
        public required Product Product { get; set; }
    }
}
