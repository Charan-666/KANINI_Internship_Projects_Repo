using System.ComponentModel.DataAnnotations;

namespace webapiAssesment.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get;set; }

        [Required, StringLength(50)]
        public string? Name { get; set; }

        [Required, StringLength(50), EmailAddress]
        public string? Email { get; set; }

        [Required, StringLength(50)]
        public string? Role { get; set; }

        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }

        public ICollection<EmployeeProject>? EmployeeProjects { get; set; } = new List<EmployeeProject>();


    }
}
