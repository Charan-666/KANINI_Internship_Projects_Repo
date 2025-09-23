using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smart_Complaint_Registration.Dto;
using Smart_Complaint_Registration.Models;
using Smart_Complaint_Registration.Services;
using static Smart_Complaint_Registration.Models.User;

namespace Smart_Complaint_Registration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly DocumentService _documentService;
        private readonly IWebHostEnvironment _env;

        public DocumentController(DocumentService documentService, IWebHostEnvironment env)
        {
            _documentService = documentService;
            _env = env;
        }

        
        [HttpPost("upload/{complaintId}")]
        [Authorize(Roles = "Head,Agent,Citizen")]
        public async Task<IActionResult> Upload(int complaintId, IFormFile file, int userId, string role)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File not selected.");

            var uploadsPath = Path.Combine(_env.WebRootPath, "uploads/documents");
            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            var filePath = Path.Combine(uploadsPath, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var document = new DocumentEvidence
            {
                FileName = file.FileName,
                FilePath = $"/uploads/documents/{file.FileName}",
                FileType = file.ContentType,
                UploadedAt = DateTime.UtcNow,
                UploadedByUserId = userId,
                UploadedByRole = role
            };

            var savedDoc = await _documentService.AddDocumentAsync(document, complaintId);
            return Ok(savedDoc);
        }

       
        [HttpGet("complaint/{complaintId}")]
        [Authorize(Roles = "Head,Agent,Admin")]
        public async Task<IActionResult> GetDocumentsByComplaint(int complaintId)
        {
            var documents = await _documentService.GetDocumentsByComplaintAsync(complaintId);
            return Ok(documents);
        }

      
        [HttpGet("download/{documentId}")]
        [Authorize(Roles = "Head,Agent,Admin,Citizen")]
        public async Task<IActionResult> DownloadDocument(int documentId)
        {
            var document = await _documentService.GetDocumentByIdAsync(documentId);
            if (document == null)
                return NotFound();

            var filePath = Path.Combine(_env.WebRootPath, "uploads/documents", document.FileName);
            if (!System.IO.File.Exists(filePath))
                return NotFound("File not found on server.");

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, document.FileType, document.FileName);
        }
    }
}