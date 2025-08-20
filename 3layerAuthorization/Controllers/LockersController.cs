using Authorizationwebapi.Services;
using Authorizationwebapis.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authorizationwebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LockersController : ControllerBase
    {
        private readonly LockerService _lockerService;

        public LockersController(LockerService lockerService)
        {
            _lockerService = lockerService;
        }

        // GET: api/Lockers
        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public async Task<ActionResult<IEnumerable<Locker>>> GetLockers()
        {
            var lockers = await _lockerService.GetAllLockersAsync();
            return Ok(lockers);
        }

        // GET: api/Lockers/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin,user")]
        public async Task<ActionResult<Locker>> GetLocker(int id)
        {
            var locker = await _lockerService.GetLockerByIdAsync(id);

            if (locker == null)
            {
                return NotFound();
            }

            return locker;
        }

        // PUT: api/Lockers/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PutLocker(int id, Locker locker)
        {
            if (id != locker.Id)
            {
                return BadRequest();
            }

            try
            {
                await _lockerService.UpdateLockerAsync(locker);
            }
            catch
            {
                if (!await _lockerService.LockerExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Lockers
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Locker>> PostLocker(Locker locker)
        {
            await _lockerService.AddLockerAsync(locker);
            return CreatedAtAction("GetLocker", new { id = locker.Id }, locker);
        }

        // DELETE: api/Lockers/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteLocker(int id)
        {
            var locker = await _lockerService.GetLockerByIdAsync(id);
            if (locker == null)
            {
                return NotFound();
            }

            await _lockerService.DeleteLockerAsync(id);
            return NoContent();
        }
    }
}