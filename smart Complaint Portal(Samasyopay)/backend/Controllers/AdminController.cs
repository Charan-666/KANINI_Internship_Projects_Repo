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
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _service;

        public AdminController(IAdminService service)
        {
            _service = service;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers([FromQuery] UserRole? role)
        {
            var users = await _service.GetAllUsersAsync(role);
            return Ok(users);
        }

        [HttpPost("add-agent")]
        public async Task<IActionResult> AddAgent([FromBody] AddAgentDTO dto)
        {
            // Map DTO to User
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = dto.Password,
                Role = UserRole.Agent
            };

            // Map DTO to Agent
            var agent = new Agent
            {
                Department = dto.Department,
                
                User = user
            };

            await _service.AddAgentAsync(user, agent);
            return Ok(new { message = "Agent added successfully" });
        }

        [HttpPut("agents/{agentId}")]
        public async Task<IActionResult> UpdateAgent(int agentId, [FromBody] UpdateAgentDTO dto)
        {
            var updatedUser = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = dto.Password,
                Role = UserRole.Agent
            };

            var updatedAgent = new Agent
            {
                Department = dto.Department
            };

            var user = await _service.UpdateAgentAsync(agentId, updatedUser, updatedAgent);
            return Ok(user);
        }


        [HttpDelete("agents/{agentId}")]
        public async Task<IActionResult> DeleteAgent(int agentId)
        {
            var success = await _service.DeleteAgentAsync(agentId);
            return success ? Ok() : BadRequest("Agent not found or cannot delete");
        }

        [HttpDelete("users/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var success = await _service.DeleteUserAsync(userId);
            return success ? Ok() : BadRequest("User not found or cannot delete");
        }

        [HttpGet("complaints")]
        public async Task<IActionResult> GetAllComplaints()
        {
            var complaints = await _service.GetAllComplaintsAsync();
            return Ok(complaints);
        }

        [HttpGet("complaints/{complaintId}")]
        public async Task<IActionResult> GetComplaintDetails(int complaintId)
        {
            var complaint = await _service.GetComplaintDetailsAsync(complaintId);
            return complaint == null ? NotFound() : Ok(complaint);
        }

        [HttpPut("complaints/{complaintId}/assign/{agentId}")]
        public async Task<IActionResult> AssignComplaint(int complaintId, int agentId)
        {
            var complaint = await _service.AssignComplaintAsync(complaintId, agentId);
            return Ok(complaint);
        }

        [HttpGet("complaints/search")]
        public async Task<IActionResult> SearchComplaints([FromQuery] int? typeId, [FromQuery] DateTime? from, [FromQuery] DateTime? to,
                                                          [FromQuery] ComplaintStatus? status, [FromQuery] int? agentId, [FromQuery] int? citizenId)
        {
            var complaints = await _service.SearchComplaintsAsync(typeId, from, to, status, agentId, citizenId);
            return Ok(complaints);
        }

        // Complaint Type Management
        [HttpGet("complaint-types")]
        public async Task<IActionResult> GetAllComplaintTypes()
        {
            var types = await _service.GetAllComplaintTypesAsync();
            return Ok(types);
        }

        [HttpPost("complaint-types")]
        public async Task<IActionResult> CreateComplaintType([FromBody] CreateComplaintTypeDTO dto)
        {
            var type = await _service.CreateComplaintTypeAsync(dto.Name, dto.TypeName);
            return Ok(type);
        }

        [HttpPut("complaint-types/{id}")]
        public async Task<IActionResult> UpdateComplaintType(int id, [FromBody] CreateComplaintTypeDTO dto)
        {
            var type = await _service.UpdateComplaintTypeAsync(id, dto.Name, dto.TypeName);
            return type == null ? NotFound() : Ok(type);
        }

        [HttpDelete("complaint-types/{id}")]
        public async Task<IActionResult> DeleteComplaintType(int id)
        {
            var success = await _service.DeleteComplaintTypeAsync(id);
            return success ? Ok() : NotFound();
        }
    }
}
