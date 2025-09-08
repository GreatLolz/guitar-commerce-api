namespace GuitarCommerceAPI.Models.Cart
{
    public class CartItemDTO
    {
        public required string ProductId { get; set; }
        public required int Quantity { get; set; }
    }
}
