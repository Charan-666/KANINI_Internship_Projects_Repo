using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Authorizationwebapis.Models;
using Microsoft.AspNetCore.Authorization;

namespace Authorizationwebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LockersController : ControllerBase
    {
        private readonly LockerDbContext _context;

        public LockersController(LockerDbContext context)
        {
            _context = context;
        }

        // GET: api/Lockers
        [HttpGet]
        [Authorize(Roles ="admin,user")]
        public async Task<ActionResult<IEnumerable<Locker>>> GetLockers()
        {
            return await _context.Lockers.ToListAsync();
        }

        // GET: api/Lockers/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin,user")]
        public async Task<ActionResult<Locker>> GetLocker(int id)
        {
            var locker = await _context.Lockers.FindAsync(id);

            if (locker == null)
            {
                return NotFound();
            }

            return locker;
        }

        // PUT: api/Lockers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PutLocker(int id, Locker locker)
        {
            if (id != locker.Id)
            {
                return BadRequest();
            }

            _context.Entry(locker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LockerExists(id))
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Locker>> PostLocker(Locker locker)
        {
            _context.Lockers.Add(locker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocker", new { id = locker.Id }, locker);
        }

        // DELETE: api/Lockers/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteLocker(int id)
        {
            var locker = await _context.Lockers.FindAsync(id);
            if (locker == null)
            {
                return NotFound();
            }

            _context.Lockers.Remove(locker);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LockerExists(int id)
        {
            return _context.Lockers.Any(e => e.Id == id);
        }
    }
}
