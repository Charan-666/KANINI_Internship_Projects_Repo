using Complaint_2._0.dto;
using Complaint_2._0.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Complaint_2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ComplaintController : ControllerBase
    {
        private readonly IComplaintService _service;

        public ComplaintController(IComplaintService service)
        {
            _service = service;
        }

        // Citizen creates a complaint (multipart/form-data if files are included)
        [HttpPost("create/{citizenId}")]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "Citizen")]
        public async Task<IActionResult> CreateComplaint(int citizenId, [FromForm] CreateComplaintDTO dto)
        {
            try
            {
                var complaint = await _service.CreateComplaintAsync(citizenId, dto);
                return Ok(new { message = "Complaint created", complaintId = complaint.Id });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Get complaints for a citizen
        [HttpGet("citizen/{citizenId}")]
        [Authorize(Roles = "Citizen,Admin")]
        public async Task<IActionResult> GetByCitizen(int citizenId)
        {
            var complaints = await _service.GetComplaintsByCitizenAsync(citizenId);
            return Ok(complaints);
        }

        // Get complaints assigned to an agent
        [HttpGet("agent/{agentId}")]
        [Authorize(Roles = "Agent,Admin")]
        public async Task<IActionResult> GetByAgent(int agentId)
        {
            var complaints = await _service.GetComplaintsByAgentAsync(agentId);
            return Ok(complaints);
        }

        // Get complaint details by id
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Agent,Citizen")]
        public async Task<IActionResult> GetById(int id)
        {
            var complaint = await _service.GetComplaintByIdAsync(id);
            if (complaint == null) return NotFound(new { message = "Complaint not found" });
            return Ok(complaint);
        }

        // Update complaint status (agent uploads solution document optionally)
        [HttpPut("update-status")]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "Agent,Admin")]
        public async Task<IActionResult> UpdateStatus([FromForm] UpdateComplaintStatusDTO dto)
        {
            try
            {
                var updated = await _service.UpdateComplaintStatusAsync(dto);
                return Ok(new { message = "Status updated", complaintId = updated.Id, status = updated.Status.ToString() });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Assign a complaint to an agent (admin action)
        [HttpPut("assign")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Assign([FromBody] AssignComplaintDTO dto)
        {
            try
            {
                var assigned = await _service.AssignComplaintAsync(dto);
                return Ok(new { message = "Complaint assigned", complaintId = assigned.Id, agentId = assigned.AgentId });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Delete complaint (citizen/admin) — only deletes if repository/service permit it
        [HttpDelete("{id}")]
        [Authorize(Roles = "Citizen,Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteComplaintAsync(id);
                return Ok(new { message = "Complaint deleted" });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Get all complaints (admin)
        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var all = await _service.GetAllComplaintsAsync();
            return Ok(all);
        }
    }
}
