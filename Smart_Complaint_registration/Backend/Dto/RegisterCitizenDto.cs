namespace Smart_Complaint_Registration.Dto
{
    public class RegisterCitizenDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string AadhaarNumber { get; set; }
        public string Password { get; set; }
        public IFormFile? ProfilePhoto { get; init; }

    }
}
