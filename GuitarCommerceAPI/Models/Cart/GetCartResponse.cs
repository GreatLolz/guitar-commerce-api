namespace GuitarCommerceAPI.Models.Cart
{
    public class GetCartResponse
    {
        public required List<CartItemDTO> CartItems { get; set; }
    }
}
