namespace Smart_Complaint_Registration.Models
{
    public class ComplaintDocumentEvidence
    {
        public int Id { get; set; }
        public int ComplaintId { get; set; }
        public int DocumentEvidenceId { get; set; }

        public Complaint Complaint { get; set; }
        public DocumentEvidence DocumentEvidence { get; set; }
    }
}
