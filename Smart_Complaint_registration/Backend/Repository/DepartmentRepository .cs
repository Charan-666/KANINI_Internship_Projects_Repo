using Microsoft.EntityFrameworkCore;
using Smart_Complaint_Registration.Data;
using Smart_Complaint_Registration.Interfaces;
using Smart_Complaint_Registration.Models;

namespace Smart_Complaint_Registration.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly SmartDbContext _context;
        private readonly DbSet<Department> _dbSet;

        public DepartmentRepository(SmartDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Department>();
        }

        public async Task<Department> AddAsync(Department entity)
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

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _dbSet
                .Include(d => d.DepartmentUsers)
                .Include(d => d.ComplaintDepartments)
                .ToListAsync();
        }

        public async Task<Department?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(d => d.DepartmentUsers)
                .Include(d => d.ComplaintDepartments)
                .FirstOrDefaultAsync(d => d.DepartmentId == id);
        }

        public async Task<Department> UpdateAsync(Department entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
    }
