using Microsoft.EntityFrameworkCore;
using Smart_Complaint_Registration.Data;
using Smart_Complaint_Registration.Interfaces;
using Smart_Complaint_Registration.Models;

namespace Smart_Complaint_Registration.Repository
{
    public class CitizenRepository : ICitizenRepository
    {
        private readonly SmartDbContext _context;
        private readonly DbSet<Citizen> _dbSet;

        public CitizenRepository(SmartDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Citizen>();
        }

        public async Task<IEnumerable<Citizen>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Citizen?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<Citizen> AddAsync(Citizen entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Citizen> UpdateAsync(Citizen entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return false;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
