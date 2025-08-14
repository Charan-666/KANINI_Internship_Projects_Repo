using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeTrackerAPI.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required, StringLength(8)]
        public string EmployeeCode { get; set; }

        [Required, StringLength(150)]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required, StringLength(50)]
        public string Designation { get; set; }

        [Required, Precision(18, 2)]
        public decimal Salary { get; set; }

        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]

        public Project? Project { get; set; }

    }
}
