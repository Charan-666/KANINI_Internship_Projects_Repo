using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace onetomany.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public int AuthorId { get; set; } // Foreign Key

        [ForeignKey("AuthorId")]
        public virtual Author Author { get; set; } // Navigation Property
    }
}
