namespace Smart_Complaint_Registration.Dto
{
    public class CitizenResponseDto
    {
        public int CitizenId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? ProfilePhotoBase64 { get; set; }
    }
}
