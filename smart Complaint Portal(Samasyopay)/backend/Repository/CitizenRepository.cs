using Complaint_2._0.data;
using Complaint_2._0.Models;
using Microsoft.EntityFrameworkCore;

namespace Complaint_2._0.Repository
{
    public class CitizenRepository : ICitizenRepository
    {
        private readonly AppDbContext _context;

        public CitizenRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Citizen> GetProfileAsync(int citizenId) =>
            await _context.Citizens.Include(c => c.User).FirstOrDefaultAsync(c => c.UserId == citizenId);

        public async Task<Citizen> UpdateProfileAsync(Citizen citizen)
        {
            _context.Citizens.Update(citizen);
            await _context.SaveChangesAsync();
            return citizen;
        }

        public async Task<Complaint> CreateComplaintAsync(Complaint complaint)
        {
            _context.Complaints.Add(complaint);
            await _context.SaveChangesAsync();
            return complaint;
        }

        public async Task<IEnumerable<Complaint>> GetMyComplaintsAsync(int citizenId) =>
            await _context.Complaints
                .Include(c => c.ComplaintType)
                .Include(c => c.ComplaintDocuments)
                .Where(c => c.CitizenId == citizenId)
                .ToListAsync();

        public async Task<Complaint> GetComplaintDetailsAsync(int citizenId, int complaintId) =>
            await _context.Complaints
                .Include(c => c.ComplaintType)
                .Include(c => c.ComplaintDocuments)
                .FirstOrDefaultAsync(c => c.CitizenId == citizenId && c.Id == complaintId);

        public async Task<Complaint> UpdateComplaintAsync(Complaint complaint)
        {
            _context.Complaints.Update(complaint);
            await _context.SaveChangesAsync();
            return complaint;
        }

        public async Task<bool> DeleteComplaintAsync(int citizenId, int complaintId)
        {
            var complaint = await _context.Complaints
                .FirstOrDefaultAsync(c => c.Id == complaintId && c.CitizenId == citizenId && c.AgentId == null);

            if (complaint == null) return false;

            _context.Complaints.Remove(complaint);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int?> GetAgentByComplaintTypeAsync(int complaintTypeId)
        {
            var complaintType = await _context.ComplaintTypes.FindAsync(complaintTypeId);
            if (complaintType == null) return null;

            var agent = await _context.Agents
                .FirstOrDefaultAsync(a => a.Department == complaintType.Name);
            
            return agent?.UserId;
        }
    }
}
