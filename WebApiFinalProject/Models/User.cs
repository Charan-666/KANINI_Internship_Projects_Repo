using System.ComponentModel.DataAnnotations;
using System.Data;

namespace WebApiFinalProject.Models
{
    public class User
    {
        [Key]
 
        public int Id { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; }

        // Foreign key is non-nullable.
        // One-to-Many relationship: A Role can have many Users.
        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;
    }
}
