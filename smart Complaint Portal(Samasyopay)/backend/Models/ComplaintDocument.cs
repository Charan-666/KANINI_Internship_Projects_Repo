using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Complaint_2._0.Models
{
    public enum DocumentType
    {
        Uploaded,
        Solved
    }

    public class ComplaintDocument
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ComplaintId { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string ContentType { get; set; }

        [Required]
        public byte[] Data { get; set; }        // <-- replaces FileContent

        [Required]
        public DocumentType Type { get; set; } // <-- added Type enum

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("ComplaintId")]
        public Complaint Complaint { get; set; }
    }

}