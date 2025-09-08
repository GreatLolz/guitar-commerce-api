using GuitarCommerceAPI.Models.Products;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuitarCommerceAPI.Models.Cart
{
    [Table("CartItems")]
    public class CartItem
    {
        public required int Id { get; set; }
        public required string CartId { get; set; }
        public required string ProductId { get; set; }
        public required int Quantity { get; set; }

        public required Cart Cart { get; set; }
        public required Product Product { get; set; }
    }
}
