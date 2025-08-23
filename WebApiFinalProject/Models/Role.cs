using System.ComponentModel.DataAnnotations;

namespace WebApiFinalProject.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        // Can be null.
        // One-to-Many relationship: A Role can have many Users.
        public ICollection<User>? Users { get; set; }

    }
}
