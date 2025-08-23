using System.ComponentModel.DataAnnotations;

namespace WebApiFinalProject.Models
{
    public class Feature
    {
        [Key]

        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int Price;
       

        [StringLength(500)]
        public string? Description { get; set; }

        // Foreign key is non-nullable.
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; } = null!;
    }
}
