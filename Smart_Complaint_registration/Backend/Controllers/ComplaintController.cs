using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Smart_Complaint_Registration.Dto;
using Smart_Complaint_Registration.Models;
using Smart_Complaint_Registration.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Smart_Complaint_Registration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintController : ControllerBase
    {

        private readonly ComplaintService _service;

        public ComplaintController(ComplaintService service)
        {
            _service = service;
        }

        [HttpGet]

        [Authorize(Roles = "Head,Agent,Admin,Citizen")]
        public async Task<IActionResult> GetAll()
        {
            var complaints = await _service.GetAllAsync();
            return Ok(complaints.Select(c => new ComplaintDto
            {
                

                ComplaintId = c.ComplaintId,
                Title = c.Title,
                Description = c.Description,
                Status = c.Status,
                PriorityId = c.PriorityId,
                SeverityId = c.SeverityId,
                ComplaintCategoryId = c.ComplaintCategoryId,
                CitizenId = c.CitizenId,
                CreatedDate = c.CreatedDate,
                DueDate = c.DueDate,
                ResolvedDate = c.ResolvedDate
            }));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Head,Agent,Admin,Citizen")]
        public async Task<IActionResult> GetById(int id)
        {
            var complaint = await _service.GetByIdAsync(id);
            if (complaint == null) return NotFound();

            return Ok(new ComplaintDto
            {
                ComplaintId = complaint.ComplaintId,
                Title = complaint.Title,
                Description = complaint.Description,
                Status = complaint.Status,
                PriorityId = complaint.PriorityId,
                SeverityId = complaint.SeverityId,
                ComplaintCategoryId = complaint.ComplaintCategoryId,
                CitizenId = complaint.CitizenId,
                CreatedDate = complaint.CreatedDate,
                DueDate = complaint.DueDate,
                ResolvedDate = complaint.ResolvedDate
            });
        }

        [HttpPost]
        [Authorize(Roles = "Citizen")]
        public async Task<IActionResult> Create([FromBody] CreateComplaintDto dto)
        {
            var entity = new Complaint
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status,
                PriorityId = dto.PriorityId,
                SeverityId = dto.SeverityId,
                ComplaintCategoryId = dto.ComplaintCategoryId,
                CitizenId = dto.CitizenId,
                CreatedDate = DateTime.UtcNow,
                DueDate = dto.DueDate
            };
            var created = await _service.AddAsync(entity);
            return Ok(new ComplaintDto

                { 
                ComplaintId = created.ComplaintId,
                Title = created.Title,
                Description = created.Description,
                Status = created.Status,
                PriorityId = created.PriorityId,
                SeverityId = created.SeverityId,
                ComplaintCategoryId = created.ComplaintCategoryId,
                CitizenId = created.CitizenId,
                CreatedDate = DateTime.UtcNow,
                DueDate = created.DueDate

            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Citizen")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateComplaintDto dto)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Title = dto.Title;
            existing.Description = dto.Description;
            existing.Status = dto.Status;
            existing.PriorityId = dto.PriorityId;
            existing.SeverityId = dto.SeverityId;
            existing.ComplaintCategoryId = dto.ComplaintCategoryId;
            existing.CitizenId = dto.CitizenId;
            existing.DueDate = dto.DueDate;

            var updated = await _service.UpdateAsync(existing);
            return Ok(new ComplaintDto
            { 
                ComplaintId = updated.ComplaintId ,
                Title = updated.Title,
                Description = updated.Description,
                Status = updated.Status,
                PriorityId = updated.PriorityId,
                SeverityId = updated.SeverityId,
                ComplaintCategoryId = updated.ComplaintCategoryId,
                CitizenId = updated.CitizenId,
                CreatedDate = DateTime.UtcNow,
                DueDate = updated.DueDate

            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Citizen")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }


        [HttpPost("submit")]
        [Authorize(Roles = "Citizen")]
        public async Task<IActionResult> Submit([FromBody] CreateComplaintDto dto)
        {
            var complaint = new Complaint
            {
                Title = dto.Title,
                Description = dto.Description,
                PriorityId = dto.PriorityId,
                SeverityId = dto.SeverityId,
                ComplaintCategoryId = dto.ComplaintCategoryId,
                CitizenId = dto.CitizenId,
                Status = 0 // Pending
            };

            var created = await _service.SubmitComplaintAsync(complaint);
            return Ok(new { created.ComplaintId, created.Title, created.Status });
        }


        [HttpPut("{id}/status")]
        [Authorize(Roles = "Head,Agent")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateComplaintStatusDTO dto)
        {
            var updated = await _service.UpdateComplaintStatusAsync(id, dto.Status);
            return Ok(new { updated.ComplaintId, updated.Status, updated.ResolvedDate });
        }


    }
}
