using Complaint_2._0.data;
using Complaint_2._0.Models;
using Microsoft.EntityFrameworkCore;

namespace Complaint_2._0.Repository
{
    
        public class ComplaintTypeRepository : IComplaintTypeRepository
        {
            private readonly AppDbContext _context;
            public ComplaintTypeRepository(AppDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<ComplaintType>> GetAllAsync()
            {
                return await _context.ComplaintTypes.ToListAsync();
            }

            public async Task<ComplaintType> GetByIdAsync(int id)
            {
                return await _context.ComplaintTypes.FindAsync(id);
            }

            public async Task AddAsync(ComplaintType type)
            {
                await _context.ComplaintTypes.AddAsync(type);
            }

            public async Task UpdateAsync(ComplaintType type)
            {
                _context.ComplaintTypes.Update(type);
            }

            public async Task DeleteAsync(ComplaintType type)
            {
                _context.ComplaintTypes.Remove(type);
            }

            public async Task SaveAsync()
            {
                await _context.SaveChangesAsync();
            }
        }
    }

