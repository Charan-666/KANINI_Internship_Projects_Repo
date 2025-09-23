namespace Smart_Complaint_Registration.Models
{
    public class Severity
    {
        public int SeverityId { get; set; }
        public string Name { get; set; }

        public ICollection<Complaint> Complaints { get; set; }
    }
}
