namespace Smart_Complaint_Registration.Models
{
    public class Complaint
    {
        public int ComplaintId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Status { get; set; } // Enum or int for Pending, InProgress, Resolved, Overdue
        public int PriorityId { get; set; }
        public int SeverityId { get; set; }
        public int ComplaintCategoryId { get; set; }
        public int CitizenId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ResolvedDate { get; set; }

        public Citizen Citizen { get; set; }
        public ComplaintCategory ComplaintCategory { get; set; }
        public Priority Priority { get; set; }
        public Severity Severity { get; set; }
        
        public ICollection<ComplaintDepartmentCollaboration> ComplaintDepartments { get; set; }
        public ICollection<ComplaintDocumentEvidence> ComplaintDocuments { get; set; }
       
    }
}
