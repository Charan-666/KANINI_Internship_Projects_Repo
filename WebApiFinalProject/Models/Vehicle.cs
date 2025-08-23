using System.ComponentModel.DataAnnotations;

namespace WebApiFinalProject.Models
{
    public class Vehicle
    {
        [Key]
    
        public int VehicleId { get; set; }
        [Required]
        [StringLength(100)]
        public string Make { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public string Model { get; set; } = string.Empty;
        //yyyy-mm-dd
        public DateOnly date { get; set; }

        // Foreign key to Owner. It is non-nullable to enforce the relationship.
        public int OwnerId { get; set; }
        public Owner Owner { get; set; } = null!; // Non-null reference to the Owner. The '!' tells the compiler this will not be null at runtime due to the foreign key constraint.

        // A collection of related entities can be null.
        // One-to-Many relationship: A Vehicle can have many Features.
        public ICollection<Feature>? Features { get; set; }

    }
}
