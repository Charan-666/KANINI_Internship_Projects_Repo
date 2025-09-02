using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapiAssesment.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required , StringLength(50)]
        public string? Name { get; set; }

        [Required, StringLength(100)]
        public string Location { get; set; }

        [ForeignKey("Employee")]
        public int? ManagerId { get; set; }
        

        public ICollection<Employee>? Employees { get; set; } = new List<Employee>();


    }
}
