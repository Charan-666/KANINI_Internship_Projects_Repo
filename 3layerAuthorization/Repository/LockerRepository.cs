using Authorizationwebapi.Interfaces;
using Authorizationwebapis.Models;
using Microsoft.EntityFrameworkCore;

namespace Authorizationwebapi.Repository
{
    public class LockerRepository : ILockerRepository
    {
        private readonly LockerDbContext _context;

        public LockerRepository(LockerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Locker>> GetAllLockersAsync()
        {
            return await _context.Lockers.ToListAsync();
        }

        public async Task<Locker> GetLockerByIdAsync(int id)
        {
            return await _context.Lockers.FindAsync(id);
        }

        public async Task AddLockerAsync(Locker locker)
        {
            await _context.Lockers.AddAsync(locker);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLockerAsync(Locker locker)
        {
            _context.Entry(locker).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLockerAsync(int id)
        {
            var locker = await _context.Lockers.FindAsync(id);
            if (locker != null)
            {
                _context.Lockers.Remove(locker);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> LockerExistsAsync(int id)
        {
            return await _context.Lockers.AnyAsync(e => e.Id == id);
        }
    }
}
