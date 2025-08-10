using System.ComponentModel.DataAnnotations;

namespace onetomany.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        // Navigation Property (One-to-Many)
        public virtual ICollection<Book> Books { get; set; }
    }
}
