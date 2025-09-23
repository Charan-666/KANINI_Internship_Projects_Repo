using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smart_Complaint_Registration.Dto;
using Smart_Complaint_Registration.Interfaces;
using Smart_Complaint_Registration.Models;
using Smart_Complaint_Registration.Services;

namespace Smart_Complaint_Registration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitizenController : ControllerBase
    {
        private readonly CitizenService _service;

        public CitizenController(CitizenService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Head,Agent,Admin,Citizen")]
        public async Task<IActionResult> GetAll()
        {
            var citizens = await _service.GetAllAsync();
            return Ok(citizens.Select(c => new CitizenDto
            {
                CitizenId = c.CitizenId,
                Name = c.Name,
                Email = c.Email,
                Phone = c.Phone,
                Address = c.Address,
                AadhaarNumber = c.AadhaarNumber
            }));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Head,Agent,Admin,Citizen")]
        public async Task<IActionResult> GetById(int id)
        {
            var citizen = await _service.GetByIdAsync(id);
            if (citizen == null) return NotFound();

            return Ok(new CitizenDto
            {
                CitizenId = citizen.CitizenId,
                Name = citizen.Name,
                Email = citizen.Email,
                Phone = citizen.Phone,
                Address = citizen.Address,
                AadhaarNumber = citizen.AadhaarNumber
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Citizen")]
        public async Task<IActionResult> Create([FromBody] CitizenDto dto)
        {
            var entity = new Citizen
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address,
                AadhaarNumber = dto.AadhaarNumber
            };

            var created = await _service.AddAsync(entity);

            return Ok(new CitizenDto
            {
                CitizenId = created.CitizenId,
                Name = created.Name,
                Email = created.Email,
                Phone = created.Phone,
                Address = created.Address,
                AadhaarNumber = created.AadhaarNumber
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Citizen")]
        public async Task<IActionResult> Update(int id, [FromBody] CitizenDto dto)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Name = dto.Name;
            existing.Email = dto.Email;
            existing.Phone = dto.Phone;
            existing.Address = dto.Address;
            existing.AadhaarNumber = dto.AadhaarNumber;

            var updated = await _service.UpdateAsync(existing);

            return Ok(new CitizenDto
            {
                CitizenId = updated.CitizenId,
                Name = updated.Name,
                Email = updated.Email,
                Phone = updated.Phone,
                Address = updated.Address,
                AadhaarNumber = updated.AadhaarNumber
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Citizen")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }

       
    }
}
