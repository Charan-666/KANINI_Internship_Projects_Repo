using Complaint_2._0.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Complaint_2._0.Models
{
    public class Citizen
    {
        [Key, ForeignKey("User")]
        public int UserId { get; set; }

        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public User User { get; set; }
        public ICollection<Complaint> Complaints { get; set; }
    }
}




      
