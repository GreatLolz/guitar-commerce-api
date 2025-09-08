using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuitarCommerceAPI.Models
{
    [Table("Users")]
    public class User
    {
        public required string Id { get; set; }

        [Required, MaxLength(100)]
        public required string Name { get; set; }

        [Required, MaxLength(256)]
        public required string PasswordHash { get; set; }
    }
}
