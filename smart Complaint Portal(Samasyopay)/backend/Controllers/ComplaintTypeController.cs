using Complaint_2._0.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Complaint_2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ComplaintTypeController : ControllerBase
    {
        private readonly IComplaintTypeService _service;
        public ComplaintTypeController(IComplaintTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Agent,Citizen")]
        public async Task<IActionResult>GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Agent,Citizen")]
        public async Task<IActionResult> GetById(int id)
        {
            var type = await _service.GetByIdAsync(id);
            if (type == null) return NotFound();
            return Ok(type);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] string typeName)
        {
            var type = await _service.CreateAsync(typeName);
            return Ok(type);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] string newName)
        {
            var updated = await _service.UpdateAsync(id, newName);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return Ok(new { message = "Complaint type deleted" });
        }
    }
}
