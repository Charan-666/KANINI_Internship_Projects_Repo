using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smart_Complaint_Registration.Interfaces;
using Smart_Complaint_Registration.Models;

namespace Smart_Complaint_Registration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriorityController : ControllerBase
    {
        private readonly IBaseRepository<Priority> _repository;

        public PriorityController(IBaseRepository<Priority> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Authorize(Roles = "Head,Agent,Admin")]
        public async Task<IActionResult> GetAll()
        {
            var priorities = await _repository.GetAllAsync();
            return Ok(priorities);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Head,Agent,Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var priority = await _repository.GetByIdAsync(id);
            if (priority == null) return NotFound();
            return Ok(priority);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] Priority priority)
        {
            var created = await _repository.AddAsync(priority);
            return CreatedAtAction(nameof(GetById), new { id = created.PriorityId }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] Priority priority)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Name = priority.Name;
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
