using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smart_Complaint_Registration.Interfaces;
using Smart_Complaint_Registration.Models;

namespace Smart_Complaint_Registration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeverityController : ControllerBase
    {
        private readonly IBaseRepository<Severity> _repository;

        public SeverityController(IBaseRepository<Severity> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Authorize(Roles = "Head,Agent,Admin")]
        public async Task<IActionResult> GetAll()
        {
            var severities = await _repository.GetAllAsync();
            return Ok(severities);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Head,Agent,Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var severity = await _repository.GetByIdAsync(id);
            if (severity == null) return NotFound();
            return Ok(severity);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] Severity severity)
        {
            var created = await _repository.AddAsync(severity);
            return CreatedAtAction(nameof(GetById), new { id = created.SeverityId }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] Severity severity)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Name = severity.Name;
            var updated = await _repository.UpdateAsync(existing);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
