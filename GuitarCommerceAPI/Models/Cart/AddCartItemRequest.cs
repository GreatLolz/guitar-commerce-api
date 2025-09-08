namespace GuitarCommerceAPI.Models.Cart
{
    public class AddCartItemRequest
    {
        public required string ProductId { get; set; }
        public required int Quantity { get; set; }
    }
}
