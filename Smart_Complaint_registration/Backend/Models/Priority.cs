namespace Smart_Complaint_Registration.Models
{
    public class Priority
    {
        public int PriorityId { get; set; }
        public string Name { get; set; }

        public ICollection<Complaint> Complaints { get; set; }
    }
}
