using Smart_Complaint_Registration.Models;

namespace Smart_Complaint_Registration.Dto
{
    public class CreateUserDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public User.UserRole Role { get; set; }
        public int? DepartmentId { get; set; } // Required only for Head/Agent
    }
}
