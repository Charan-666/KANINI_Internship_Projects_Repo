using Complaint_2._0.Models;
using System.ComponentModel.DataAnnotations;

namespace Complaint_2._0.dto
{
    public class UpdateComplaintStatusDTO
    {
        [Required]
        public int ComplaintId { get; set; }

        [Required]
        public ComplaintStatus Status { get; set; }

        public IFormFile SolutionDocument { get; set; } // uploaded by agent
    }
}
