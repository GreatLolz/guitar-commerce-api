using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GuitarCommerceAPI.Models.Order
{
    [Owned]
    public class ShippingAddress
    {
        [Required]
        public required string Email { get; set; }

        [Required]
        public required string PhoneNumber { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string AddressLine1 { get; set; }


        public string AddressLine2 { get; set; } = string.Empty;

        [Required]
        public required string City { get; set; }

        [Required]
        public required string Country { get; set; }

        [Required]
        public required string ZipCode { get; set; }
    }
}
