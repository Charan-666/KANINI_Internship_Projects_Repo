using Complaint_2._0.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Complaint_2._0.Models
{
    public class Agent
    {
        [Key, ForeignKey("User")]
        public int UserId { get; set; }

        public string Department { get; set; } // e.g., Aadhar, Voter, BirthCert

        public User User { get; set; }
        public ICollection<Complaint> AssignedComplaints { get; set; }
    }
}


