using Complaint_2._0.Models;
using System.ComponentModel.DataAnnotations;

namespace Complaint_2._0.Models
{
    public class ComplaintType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } // e.g., Aadhar, Voter, BirthCertificate, etc.

        [Required]
        public string TypeName { get; set; }
        // Navigation
        public ICollection<Complaint> Complaints { get; set; }

    }
}


