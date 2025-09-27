using System.ComponentModel.DataAnnotations;

namespace Complaint_2._0.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
