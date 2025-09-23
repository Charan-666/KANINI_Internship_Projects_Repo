namespace Smart_Complaint_Registration.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }     
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }

        // Nullable foreign keys to link to domain-specific profiles
        public int? CitizenId { get; set; }
        public int? DepartmentUserId { get; set; }

        public Citizen Citizen { get; set; }
        public DepartmentUser DepartmentUser { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime? CreatedAt { get; set; } 
        public DateTime? LastLoginAt { get; set; }




        public enum UserRole
        {
            Citizen = 1,
            Agent = 2,
            Head = 3,
            Admin = 4
        }
    }
}
