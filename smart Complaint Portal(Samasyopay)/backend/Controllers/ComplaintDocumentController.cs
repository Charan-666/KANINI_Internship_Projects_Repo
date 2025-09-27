using Complaint_2._0.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Complaint_2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ComplaintDocumentController : ControllerBase
    {
        private readonly IComplaintDocumentService _service;
        public ComplaintDocumentController(IComplaintDocumentService service)
        {
            _service = service;
        }

        // Download document by ID
        [HttpGet("download/{id}")]
        [Authorize(Roles = "Admin,Agent,Citizen")]
        public async Task<IActionResult> Download(int id)
        {
            var doc = await _service.GetByIdAsync(id);
            if (doc == null) return NotFound(new { message = "Document not found" });

            return File(doc.Data, doc.ContentType, doc.FileName);
        }
    }
}
