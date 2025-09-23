namespace Smart_Complaint_Registration.Dto
{
    public class CitizenDto
    {
        public int CitizenId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string AadhaarNumber { get; set; } = string.Empty;
    }
}
