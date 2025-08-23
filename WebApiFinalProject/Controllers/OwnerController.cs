using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiFinalProject.DTOs;
using WebApiFinalProject.Models;
using WebApiFinalProject.Services;

namespace WebApiFinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    
    public class OwnerController : ControllerBase
    {
        private readonly OwnerService _ownerService;

        public OwnerController(OwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAll()
        {
            var owners = await _ownerService.GetAllAsync();
            return Ok(owners.Select(o => new OwnerDTO
            {
                OwnerId = o.OwnerId,
                Name = o.Name,
                Email = o.Email,
                Age = o.Age,
                Address = o.Address
            }));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetById(int id)
        {
            var owner = await _ownerService.GetByIdAsync(id);
            if (owner == null) return NotFound();

            return Ok(new OwnerDTO
            {
                OwnerId = owner.OwnerId,
                Name = owner.Name,
                Email = owner.Email,
                Age= owner.Age,
                Address = owner.Address
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateOwnerDTO dto)
        {
            var owner = new Owner { Name = dto.Name,Email=dto.Email,Age=dto.Age, Address = dto.Address };
            var created = await _ownerService.AddAsync(owner);

            return Ok(new OwnerDTO
            {
                OwnerId = created.OwnerId,
                Name = created.Name,
                Email = created.Email,
                Age = created.Age,
                Address = created.Address
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateOwnerDTO dto)
        {
            var existing = await _ownerService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Name = dto.Name;
            existing.Email = dto.Email;
            existing.Age = dto.Age;
            existing.Address = dto.Address;

            var updated = await _ownerService.UpdateAsync(existing);

            return Ok(new OwnerDTO
            {
                OwnerId = updated.OwnerId,
                Name = updated.Name,
                Email = updated.Email,
                Age = updated.Age,
                Address = updated.Address
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _ownerService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }


        //operation for search,filter,count
        [HttpGet("search")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult SearchOwners(string name)
        {
            var owners = _ownerService.SearchOwners(name);
            return Ok(owners);
        }

        [HttpGet("count")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult CountOwners()
        {
            var count = _ownerService.GetOwnerCount();
            return Ok(count);
        }

        [HttpGet("filter")]
        [Authorize(Roles = "Admin,User")]

        public IActionResult FilterOwners([FromQuery] string? name, [FromQuery] int? minAge, [FromQuery] int? maxAge)
        {
            var filtered = _ownerService.FilterOwners(o =>
                (string.IsNullOrEmpty(name) || o.Name.Contains(name)) &&
                (!minAge.HasValue || o.Age >= minAge.Value) &&
                (!maxAge.HasValue || o.Age <= maxAge.Value)
            );

            return Ok(filtered);
        }

        
        [HttpGet("count/conditional")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetConditionalOwnerCount([FromQuery] int? minAge, [FromQuery] int? maxAge, [FromQuery] string? name)
        {
            var count = _ownerService.GetConditionalOwnerCount(o =>
                (!minAge.HasValue || o.Age >= minAge.Value) &&
                (!maxAge.HasValue || o.Age <= maxAge.Value) &&
                (string.IsNullOrEmpty(name) || o.Name.Contains(name))
            );

            return Ok(count);
        }
    }

}
