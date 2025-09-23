using Microsoft.EntityFrameworkCore;
using Smart_Complaint_Registration.Data;
using Smart_Complaint_Registration.Interfaces;
using Smart_Complaint_Registration.Models;

namespace Smart_Complaint_Registration.Repository
{
    public class ComplaintRepository : IComplaintRepository
    {
        private readonly SmartDbContext _context;
        private readonly DbSet<Complaint> _dbSet;

        public ComplaintRepository(SmartDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Complaint>();
        }

        public async Task<Complaint> AddAsync(Complaint entity)
        {
            await _dbSet.AddAsync(entity);
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

        public async Task<IEnumerable<Complaint>> GetAllAsync()
        {
            return await _dbSet
                .Include(c => c.Citizen)
                .Include(c => c.ComplaintCategory)
                .Include(c => c.Priority)
                .Include(c => c.Severity)
                .Include(c => c.ComplaintDepartments)
                .Include(c => c.ComplaintDocuments)
                .ToListAsync();
        }

        public async Task<Complaint?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(c => c.Citizen)
                .Include(c => c.ComplaintCategory)
                .Include(c => c.Priority)
                .Include(c => c.Severity)
                .Include(c => c.ComplaintDepartments)
                .Include(c => c.ComplaintDocuments)
                .FirstOrDefaultAsync(c => c.ComplaintId == id);
        }

        public async Task<Complaint> UpdateAsync(Complaint entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
