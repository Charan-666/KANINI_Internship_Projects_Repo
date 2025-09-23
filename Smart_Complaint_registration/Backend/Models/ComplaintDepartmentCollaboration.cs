namespace Smart_Complaint_Registration.Models
{
    public class ComplaintDepartmentCollaboration
    {
        public int Id { get; set; }
        public int ComplaintId { get; set; }
        public int DepartmentId { get; set; }
        public string CollaborationStatus { get; set; }

        public Complaint Complaint { get; set; }
        public Department Department { get; set; }
    }
}
