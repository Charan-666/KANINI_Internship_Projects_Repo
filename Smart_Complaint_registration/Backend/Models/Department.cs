namespace Smart_Complaint_Registration.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string ContactInfo { get; set; }

        public ICollection<DepartmentUser> DepartmentUsers { get; set; }
        public ICollection<ComplaintDepartmentCollaboration> ComplaintDepartments { get; set; }

    }
}
