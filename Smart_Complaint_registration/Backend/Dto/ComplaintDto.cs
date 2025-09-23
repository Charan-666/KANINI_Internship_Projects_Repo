namespace Smart_Complaint_Registration.Dto
{
    public class ComplaintDto
    {
        public int ComplaintId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public int PriorityId { get; set; }
        public int SeverityId { get; set; }
        public int ComplaintCategoryId { get; set; }
        public int CitizenId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ResolvedDate { get; set; }
    }
}
