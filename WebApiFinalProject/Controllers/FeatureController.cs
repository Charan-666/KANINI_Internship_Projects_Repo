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
    public class FeatureController : ControllerBase
    {
        private readonly FeatureService _featureService;

        public FeatureController(FeatureService featureService)
        {
            _featureService = featureService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<IEnumerable<FeatureDTO>>> GetAll()
        {
            var features = await _featureService.GetAllAsync();
            var result = new List<FeatureDTO>();
            foreach (var f in features)
            {
                result.Add(new FeatureDTO { Id = f.Id, Name = f.Name ,Price = f.Price ,Description = f.Description});
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<FeatureDTO>> GetById(int id)
        {
            var f = await _featureService.GetByIdAsync(id);
            if (f == null) return NotFound();

            return Ok(new FeatureDTO { Id = f.Id, Name = f.Name, Price = f.Price, Description = f.Description });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<FeatureDTO>> Create(CreateFeatureDTO dto)
        {
            var f = new Feature { Name = dto.Name, Price = dto.Price ,Description = dto.Description };
            var created = await _featureService.AddAsync(f);

            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                new FeatureDTO { Id = created.Id, Name = created.Name,Price = created.Price, Description = created.Description });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<FeatureDTO>> Update(int id, CreateFeatureDTO dto)
        {
            var existing = await _featureService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Name = dto.Name;
            existing.Price = dto.Price;
            existing.Description = dto.Description;
            var updated = await _featureService.UpdateAsync(existing);

            return Ok(new FeatureDTO { Id = updated.Id, Name = updated.Name , Price = updated.Price , Description = updated.Description });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _featureService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpGet("search")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult SearchFeatures(string name)
        {
            var features = _featureService.SearchFeatures(name);
            return Ok(features);
        }

        [HttpGet("count")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult CountFeatures()
        {
            var count = _featureService.GetFeatureCount();
            return Ok(count);
        }

        //  Filter features by price range
        [HttpGet("filter-by-price")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult FilterByPrice([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice)
        {
            var features = _featureService.FilterFeatures(f => f.Price >= minPrice && f.Price <= maxPrice);
            return Ok(features);
        }

        // Count features containing a keyword in name or description
        [HttpGet("count-by-keyword")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult CountByKeyword([FromQuery] string keyword)
        {
            var count = _featureService.GetConditionalFeatureCount(f =>
                f.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                f.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase));

            return Ok(count);
        }

    }
}
