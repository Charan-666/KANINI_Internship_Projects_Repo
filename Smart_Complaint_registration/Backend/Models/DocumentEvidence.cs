using static Smart_Complaint_Registration.Models.User;

namespace Smart_Complaint_Registration.Models
{
    public class DocumentEvidence
    {
        public int DocumentEvidenceId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public DateTime UploadedAt { get; set; }


        // New: Who uploaded the document
        public int UploadedByUserId { get; set; }
        public User UploadedBy { get; set; }
        public String? UploadedByRole { get; set; }  // Citizen / Agent / Head
        public ICollection<ComplaintDocumentEvidence> ComplaintDocuments { get; set; }

    }
}
