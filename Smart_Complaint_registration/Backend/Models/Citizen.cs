namespace Smart_Complaint_Registration.Models
{
    public class Citizen
    {
        public int CitizenId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string AadhaarNumber { get; set; }
        public byte[]? ProfilePhoto { get; set; }

        public ICollection<Complaint> Complaints { get; set; }
        public User User { get; set; }
    }
}
