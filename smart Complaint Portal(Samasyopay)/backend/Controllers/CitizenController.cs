using Complaint_2._0.dto;
using Complaint_2._0.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Complaint_2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Citizen")]
    public class CitizenController : ControllerBase
    {
        private readonly ICitizenService _service;

        public CitizenController(ICitizenService service)
        {
            _service = service;
        }

        [HttpGet("{citizenId}/profile")]
        public async Task<IActionResult> GetProfile(int citizenId)
        {
            var profile = await _service.GetProfileAsync(citizenId);
            return profile == null ? NotFound() : Ok(profile);
        }

        [HttpPut("{citizenId}/profile")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateProfile(int citizenId, [FromForm] UpdateCitizenDTO dto)
        {
            var updated = await _service.UpdateProfileAsync(citizenId, dto);
            return Ok(updated);
        }

        [HttpPost("{citizenId}/complaints")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> RaiseComplaint(int citizenId, [FromForm] CreateComplaintDTO dto)
        {
            var complaint = await _service.RaiseComplaintAsync(citizenId, dto);
            return Ok(complaint);
        }

        [HttpGet("{citizenId}/complaints")]
        public async Task<IActionResult> GetMyComplaints(int citizenId)
        {
            var complaints = await _service.GetMyComplaintsAsync(citizenId);
            return Ok(complaints);
        }

        [HttpGet("{citizenId}/complaints/{complaintId}")]
        public async Task<IActionResult> GetComplaintDetails(int citizenId, int complaintId)
        {
            var complaint = await _service.GetComplaintDetailsAsync(citizenId, complaintId);
            return complaint == null ? NotFound() : Ok(complaint);
        }

        [HttpPut("{citizenId}/complaints")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateComplaint(int citizenId, [FromForm] UpdateComplaintDTO dto)
        {
            var updated = await _service.UpdateComplaintAsync(citizenId, dto);
            return Ok(updated);
        }

        [HttpDelete("{citizenId}/complaints/{complaintId}")]
        public async Task<IActionResult> DeleteComplaint(int citizenId, int complaintId)
        {
            var success = await _service.DeleteComplaintAsync(citizenId, complaintId);
            return success ? Ok() : BadRequest("Complaint cannot be deleted (already assigned).");
        }
    }
}
