using System.ComponentModel.DataAnnotations;

namespace Complaint_2._0.dto
{
    public class CreateComplaintDTO
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int ComplaintTypeId { get; set; }

        public List<IFormFile> Documents { get; set; } // citizen uploaded files
    }
}
