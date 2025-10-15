namespace GuitarCommerceAPI.Models.Order
{
    public class CheckoutData
    {
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string FullName { get; set; }
        public required string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; } = string.Empty;
        public required string City { get; set; }
        public required string Country { get; set; }
        public required string ZipCode { get; set; }
    }
}
