namespace Smart_Complaint_Registration.Dto
{
    public class UploadDocumentDto
    {

        public int ComplaintId { get; set; }
        public IFormFile File { get; set; }   // Uploaded file
    }
}
