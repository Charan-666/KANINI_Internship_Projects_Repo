using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EmployeeTrackerAPI.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required, StringLength(10)]
        public string ProjectCode { get; set; }

        [Required, StringLength(100)]
        public string ProjectName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required, Precision(18, 2)]
        public decimal Budget { get; set; }

        public IList<Employee>? Employees { get; set; }

    }
}
