using System.ComponentModel.DataAnnotations;

namespace webapiAssesment.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required, StringLength(100)]
        public string? title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ICollection<EmployeeProject>? EmployeeProjects { get; set; } = new List<EmployeeProject>();


    }
}
