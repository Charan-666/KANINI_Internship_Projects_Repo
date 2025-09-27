using Complaint_2._0.dto;
using Complaint_2._0.Models;
using Complaint_2._0.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Complaint_2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Agent")]
    public class AgentController : ControllerBase
        {
            private readonly IAgentService _service;

            public AgentController(IAgentService service)
            {
                _service = service;
            }

            [HttpGet("{agentId}/complaints")]
            public async Task<IActionResult> GetAssignedComplaints(int agentId)
            {
                var complaints = await _service.GetAssignedComplaintsAsync(agentId);
                return Ok(complaints);
            }

            [HttpGet("{agentId}/complaints/{complaintId}")]
            public async Task<IActionResult> GetComplaintDetails(int agentId, int complaintId)
            {
                var complaint = await _service.GetComplaintDetailsAsync(agentId, complaintId);
                return complaint == null ? NotFound() : Ok(complaint);
            }

            [HttpPut("{agentId}/complaints/{complaintId}/status")]
            public async Task<IActionResult> UpdateStatus(int agentId, int complaintId, [FromQuery] ComplaintStatus status)
            {
                var updated = await _service.UpdateComplaintStatusAsync(agentId, complaintId, status);
                return Ok(updated);
            }

            [HttpPost("{agentId}/complaints/{complaintId}/solution")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadSolution(int agentId, int complaintId, [FromForm] UploadDto solutionFile)
            {
            if (solutionFile == null || solutionFile.File.Length == 0)
                return BadRequest("No file uploaded.");

            var updated = await _service.UploadSolutionDocumentAsync(agentId, complaintId, solutionFile!);
                return Ok(updated);
            }

            [HttpGet("{agentId}/complaints/search")]
            public async Task<IActionResult> SearchComplaints(int agentId, [FromQuery] int? typeId, [FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] string citizenName)
            {
                var complaints = await _service.SearchComplaintsAsync(agentId, typeId, from, to, citizenName);
                return Ok(complaints);
            }
        }
    }

