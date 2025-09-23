using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Smart_Complaint_Registration.Dto;
using Smart_Complaint_Registration.Models;
using Smart_Complaint_Registration.Services;

namespace Smart_Complaint_Registration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentService _service;

        public DepartmentController(DepartmentService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Head,Agent,Admin,Citizen")]

        public async Task<IActionResult> GetAll()
        {
            var depts = await _service.GetAllAsync();
            return Ok(depts.Select(d => new DepartmentDto
            {
                DepartmentId = d.DepartmentId,
                Name = d.Name,
                ContactInfo =d.ContactInfo
                
            }));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Head,Agent,Admin,Citizen")]
        public async Task<IActionResult> GetById(int id)
        {
            var dept = await _service.GetByIdAsync(id);
            if (dept == null) return NotFound();

            return Ok(new DepartmentDto
            {
                DepartmentId = dept.DepartmentId,
                Name = dept.Name,
                ContactInfo = dept.ContactInfo
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateDepartmentDto dto)
        {
            var entity = new Department
            {
                Name = dto.Name,
                ContactInfo = dto.ContactInfo
            };

            var created = await _service.AddAsync(entity);
            return Ok(new DepartmentDto 
            {
                DepartmentId = created.DepartmentId,
                Name = created.Name,
                ContactInfo= created.ContactInfo
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateDepartmentDto dto)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Name = dto.Name;
            existing.ContactInfo = dto.ContactInfo;

            var updated = await _service.UpdateAsync(existing);
            return Ok(new DepartmentDto
            {
                DepartmentId = updated.DepartmentId,
                Name = updated.Name,
                ContactInfo = updated.ContactInfo

            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
