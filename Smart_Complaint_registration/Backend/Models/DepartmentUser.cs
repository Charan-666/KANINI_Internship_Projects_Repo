namespace Smart_Complaint_Registration.Models
{
    public class DepartmentUser
    {
        public int DepartmentUserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; } // Head/Agent
        public int DepartmentId { get; set; }

        public Department Department { get; set; }
        public ICollection<ComplaintDepartmentCollaboration> ComplaintDepartments { get; set; }
        public User User { get; set; }
    }
}
