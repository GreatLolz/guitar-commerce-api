using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuitarCommerceAPI.Models.Products
{
    [Table("Products")]
    public class Product
    {
        public required string Id { get; set; }

        [Required, MaxLength(2048)]
        public required string ImageUrl { get; set; }

        [Required, MaxLength(100)]
        public required string Name { get; set; }

        [Required, MaxLength(1000)]
        public required string Description { get; set; }

        [Required, Precision(18, 2)]
        public required decimal Price { get; set; }

        [Required, MaxLength(100)]
        public required string Category { get; set; }

        [Required, MaxLength(100)]
        public required string Brand { get; set; }

        [Required, DefaultValue(0)]
        public required int Stock { get; set; }

        [Required, Precision(3, 2), DefaultValue(0)]
        public required decimal Rating { get; set; }

        [Required, DefaultValue(0)]
        public required int ReviewsCount { get; set; }
    }
}
