namespace Smart_Complaint_Registration.Models
{
    public class ComplaintCategory
    {
        public int ComplaintCategoryId { get; set; }
        public string CategoryName { get; set; }

        public string Description { get; set; }

        public ICollection<Complaint> Complaints { get; set; }
       

    }
}
