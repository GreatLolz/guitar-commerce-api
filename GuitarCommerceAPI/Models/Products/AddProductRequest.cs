namespace GuitarCommerceAPI.Models.Products
{
    public class AddProductRequest
    {
        public required string ImageUrl { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }
        public required string Category { get; set; }
        public required string Brand { get; set; }
        public required int Stock { get; set; }
        public required decimal Rating { get; set; }
        public required int ReviewsCount { get; set; }
    }
}
