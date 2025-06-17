using System.ComponentModel.DataAnnotations.Schema;

namespace GuitarCommerceAPI.Models
{
    [Table("Users")]
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string PasswordHash { get; set; }
    }
}
