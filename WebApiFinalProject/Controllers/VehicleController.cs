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
    public class VehicleController : ControllerBase
    {
        private readonly VehicleService _vehicleService;

        public VehicleController(VehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<IEnumerable<VehicleDTO>>> GetAll()
        {
            var vehicles = await _vehicleService.GetAllAsync();
            var result = new List<VehicleDTO>();
            foreach (var v in vehicles)
            {
                result.Add(new VehicleDTO { VehicleId = v.VehicleId,Make = v.Make, Model = v.Model, date = v.date,OwnerId = v.OwnerId });
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<VehicleDTO>> GetById(int id)
        {
            var v = await _vehicleService.GetByIdAsync(id);
            if (v == null) return NotFound();

            return Ok(new VehicleDTO { VehicleId = v.VehicleId, Make = v.Make , Model = v.Model, date = v.date, OwnerId = v.OwnerId });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<VehicleDTO>> Create(CreateVehicleDTO dto)
        {
            var v = new Vehicle { Make = dto.Make,Model = dto.Model, date = dto.date , OwnerId = dto.OwnerId };
            var created = await _vehicleService.AddAsync(v);

            return CreatedAtAction(nameof(GetById), new { id = created.VehicleId },
                new VehicleDTO { VehicleId = created.VehicleId, Make = created.Make, Model = created.Model, date = created.date, OwnerId = created.OwnerId });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<VehicleDTO>> Update(int id, CreateVehicleDTO dto)
        {
            var existing = await _vehicleService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Make = dto.Make;
            existing.Model = dto.Model;
            existing.date = dto.date;
            existing.OwnerId = dto.OwnerId;

            var updated = await _vehicleService.UpdateAsync(existing);
            return Ok(new VehicleDTO { VehicleId = updated.VehicleId, Make = updated.Make,Model = updated.Model, date=updated.date,OwnerId = updated.OwnerId });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _vehicleService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpGet("by-date/{date}")]
        public IActionResult GetVehiclesByDate(DateOnly date)
        {
            var vehicles = _vehicleService.GetVehiclesByDate(date);
            if (!vehicles.Any())
            {
                return NotFound("No vehicles found for this date.");
            }
            return Ok(vehicles);
        }

        //additional

        [HttpGet("search")]
        public IActionResult SearchVehicles(string model)
        {
            var vehicles = _vehicleService.SearchVehicles(model);
            return Ok(vehicles);
        }

        [HttpGet("count")]
        public IActionResult CountVehicles()
        {
            var count = _vehicleService.GetVehicleCount();
            return Ok(count);
        }

        // GET api/vehicle/conditional-count?make=Toyota&model=Camry&date=2025-08-23
        [HttpGet("conditional-count")]
        public ActionResult<int> GetConditionalVehicleCount(
            [FromQuery] string make = null,
            [FromQuery] string model = null,
            [FromQuery] DateOnly? date = null)
        {
            var count = _vehicleService.GetConditionalVehicleCount(v =>
                (string.IsNullOrEmpty(make) || v.Make == make) &&
                (string.IsNullOrEmpty(model) || v.Model == model) &&
                (!date.HasValue || v.date == date.Value)
            );

            return Ok(count);
        }

        // GET api/vehicle/filter?make=Honda&date=2025-08-23
        [HttpGet("filter")]
        public ActionResult<IEnumerable<Vehicle>> FilterVehicles(
            [FromQuery] string make = null,
            [FromQuery] string model = null,
            [FromQuery] DateOnly? date = null)
        {
            var vehicles = _vehicleService.FilterVehicles(v =>
                (string.IsNullOrEmpty(make) || v.Make == make) &&
                (string.IsNullOrEmpty(model) || v.Model == model) &&
                (!date.HasValue || v.date == date.Value)
            );

            return Ok(vehicles);
        }
    }
}
