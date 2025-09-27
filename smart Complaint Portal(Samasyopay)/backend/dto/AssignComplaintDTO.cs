using System.ComponentModel.DataAnnotations;

namespace Complaint_2._0.dto
{
    public class AssignComplaintDTO
    {
        [Required]
        public int ComplaintId { get; set; }

        [Required]
        public int AgentId { get; set; }
    }
}
