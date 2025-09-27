using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Complaint_2._0.Models
{
    public enum UserRole
    {
        Citizen,
        Agent,
        Admin
    }

    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public UserRole Role { get; set; }

        public byte[]? Photo { get; set; } // nullable

        // Navigation properties
        public Citizen CitizenProfile { get; set; } // only if Role = Citizen
        public Agent AgentProfile { get; set; }     // only if Role = Agent
    }
}
