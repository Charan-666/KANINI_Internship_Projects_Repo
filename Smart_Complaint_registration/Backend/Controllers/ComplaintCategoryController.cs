using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smart_Complaint_Registration.Interfaces;
using Smart_Complaint_Registration.Models;

namespace Smart_Complaint_Registration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintCategoryController : ControllerBase
    {

        private readonly IBaseRepository<ComplaintCategory> _repository;

        public ComplaintCategoryController(IBaseRepository<ComplaintCategory> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Authorize(Roles = "Head,Agent,Admin,Citizen")]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _repository.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Head,Agent,Admin,Citizen")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] ComplaintCategory category)
        {
            var created = await _repository.AddAsync(category);
            return CreatedAtAction(nameof(GetById), new { id = created.ComplaintCategoryId }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] ComplaintCategory category)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.CategoryName = category.CategoryName;
            existing.Description = category.Description;

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
