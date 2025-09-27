using Complaint_2._0.data;
using Complaint_2._0.Models;
using Microsoft.EntityFrameworkCore;

namespace Complaint_2._0.Repository
{
    public class ComplaintRepository : IComplaintRepository
    {
        private readonly AppDbContext _context;

        public ComplaintRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Complaint> AddAsync(Complaint complaint)
        {
            complaint.Status = ComplaintStatus.Pending;
            complaint.AgentId = null;

            _context.Complaints.Add(complaint);
            await _context.SaveChangesAsync();
            return complaint;
        }

        public async Task DeleteAsync(Complaint complaint)
        {
            _context.Complaints.Remove(complaint);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Complaint>> GetAllAsync()
        {
            return await _context.Complaints
                .Include(c => c.Citizen)
                .Include(c => c.Agent)
                .Include(c => c.ComplaintType)
                .ToListAsync();
        }


        public async Task<Complaint> GetByIdAsync(int id)
        {
            return await _context.Complaints
                .Include(c => c.Citizen)
                .Include(c => c.Agent)
                .Include(c => c.ComplaintType)
                .Include(c => c.ComplaintDocuments)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Complaint>> GetByCitizenIdAsync(int citizenId)
        {
            return await _context.Complaints
                .Where(c => c.CitizenId == citizenId)
                .Include(c => c.ComplaintType)
                .Include(c => c.ComplaintDocuments)
                .ToListAsync();
        }

        public async Task<List<Complaint>> GetByAgentIdAsync(int agentId)
        {
            return await _context.Complaints
                .Where(c => c.AgentId == agentId)
                .Include(c => c.Citizen)
                .Include(c => c.ComplaintType)
                .Include(c => c.ComplaintDocuments)
                .ToListAsync();
        }

        public async Task<Complaint> UpdateAsync(Complaint complaint)
        {
            _context.Complaints.Update(complaint);
            await _context.SaveChangesAsync();
            return complaint;
        }
    }

}
