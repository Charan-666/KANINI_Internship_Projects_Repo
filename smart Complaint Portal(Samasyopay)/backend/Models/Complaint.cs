using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Complaint_2._0.Models
{

        public enum ComplaintStatus
        {
        Pending,
        Assigned,      // added
        InProgress,
        Resolved
    }

        public class Complaint
        {
            [Key]
            public int Id { get; set; }

            [Required]
            public string Title { get; set; }

            public string Description { get; set; }

            [Required]
            public int ComplaintTypeId { get; set; } // FK to ComplaintType

            [Required]
            public ComplaintStatus Status { get; set; } = ComplaintStatus.Pending;

            [Required]
            public int CitizenId { get; set; }

            public int? AgentId { get; set; }

            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
            public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

            // Navigation properties
            [ForeignKey("CitizenId")]
            public Citizen Citizen { get; set; }

            [ForeignKey("AgentId")]
            public Agent Agent { get; set; }

            [ForeignKey("ComplaintTypeId")]
            public ComplaintType ComplaintType { get; set; }

            public ICollection<ComplaintDocument> ComplaintDocuments { get; set; }
        }
    }

