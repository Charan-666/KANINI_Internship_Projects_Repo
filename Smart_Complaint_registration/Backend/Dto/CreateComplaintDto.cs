namespace Smart_Complaint_Registration.Dto
{
    public class CreateComplaintDto
    {
        public string Title { get; init; }
        public string Description { get; init; }
        public int Status { get; init; }
        public int PriorityId { get; init; }
        public int SeverityId { get; init; }
        public int ComplaintCategoryId { get; init; }
        public int CitizenId { get; init; }
        public DateTime? DueDate { get; init; }

    }
}
