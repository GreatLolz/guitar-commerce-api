namespace GuitarCommerceAPI.Models.Cart
{
    public class CartItemDTO
    {
        public required string CartItemId { get; set; }
        public required string ProductId { get; set; }
        public required int Quantity { get; set; }
    }
}
