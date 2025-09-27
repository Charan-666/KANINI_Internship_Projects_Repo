namespace Complaint_2._0.dto
{
    public class UpdateCitizenDTO
    {
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public IFormFile Photo { get; set; } // optional new photo
    }
}
