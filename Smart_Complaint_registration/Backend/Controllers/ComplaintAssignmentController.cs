using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smart_Complaint_Registration.Data;
using Smart_Complaint_Registration.Dto;
using Smart_Complaint_Registration.Services;

namespace Smart_Complaint_Registration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintAssignmentController : ControllerBase
    {
        private readonly ComplaintAssignmentService _assignmentService;
        private readonly SmartDbContext _context;

        public ComplaintAssignmentController(ComplaintAssignmentService assignmentService, SmartDbContext context)
        {
            _assignmentService = assignmentService;
            _context = context;
        }

        // Assign complaint to a department (Admin action)
        [HttpPost("assign")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Assign([FromBody] AssignComplaintDTO dto)
        {
            await _assignmentService.AssignComplaintToDepartment(dto.ComplaintId, dto.DepartmentId);
            return Ok(new { Message = "Complaint assigned successfully." });
        }

        // Get complaints assigned to the logged-in department user
        [HttpGet("my-department")]
        [Authorize(Roles = "Head,Agent,Admin")]
        public async Task<IActionResult> GetDepartmentComplaints()
        {
            // Get logged-in user's DepartmentUserId
            var userId = int.Parse(User.FindFirst("UserId")?.Value!);
            var departmentUser = await _context.Users
                .Where(u => u.UserId == userId)
                .Select(u => u.DepartmentUserId)
                .FirstOrDefaultAsync();

            if (departmentUser == null) return Unauthorized("User not linked to any department.");

            var departmentId = await _context.DepartmentUsers
                .Where(d => d.DepartmentUserId == departmentUser)
                .Select(d => d.DepartmentId)
                .FirstOrDefaultAsync();

            var complaints = await _context.ComplaintDepartmentCollaborations
                .Where(c => c.DepartmentId == departmentId)
                .Include(c => c.Complaint)
                    .ThenInclude(cmp => cmp.Citizen)
                .Include(c => c.Complaint)
                    .ThenInclude(cmp => cmp.Priority)
                .Include(c => c.Complaint)
                    .ThenInclude(cmp => cmp.Severity)
                .Include(c => c.Complaint)
                    .ThenInclude(cmp => cmp.ComplaintCategory)
                .ToListAsync();

            return Ok(complaints);
        }

        // Update collaboration status (Head/Agent action)
        [HttpPut("update-status/{collaborationId}")]
        [Authorize(Roles = "Head,Agent,Admin")]
        public async Task<IActionResult> UpdateStatus(int collaborationId, [FromBody] UpdateCollaborationStatusDTO dto)
        {
            await _assignmentService.UpdateCollaborationStatus(collaborationId, dto.Status);
            return Ok(new { Message = "Collaboration status updated." });
        }
    }
}
