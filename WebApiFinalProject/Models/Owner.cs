using System.ComponentModel.DataAnnotations;

namespace WebApiFinalProject.Models
{
    public class Owner
    {
        [Key]
        public int OwnerId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty; // Initialized to prevent null warnings
        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        public int Age { get; set; }

        [StringLength(200)]
        public string? Address { get; set; } // Can be null

        // A collection of related entities can be null.
        // One-to-Many relationship: An Owner can have many Vehicles.
        public ICollection<Vehicle>? Vehicles { get; set; }

    }
}
