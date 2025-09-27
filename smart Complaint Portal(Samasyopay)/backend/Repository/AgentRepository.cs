using Complaint_2._0.data;
using Complaint_2._0.Models;
using Microsoft.EntityFrameworkCore;

namespace Complaint_2._0.Repository
{
   
        public class AgentRepository : IAgentRepository
        {
            private readonly AppDbContext _context;

            public AgentRepository(AppDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Complaint>> GetAssignedComplaintsAsync(int agentId) =>
                await _context.Complaints
                    .Include(c => c.Citizen)
                        .ThenInclude(c => c.User)
                    .Include(c => c.ComplaintType)
                    .Include(c => c.ComplaintDocuments)
                    .Where(c => c.AgentId == agentId)
                    .ToListAsync();

            public async Task<Complaint> GetComplaintDetailsAsync(int agentId, int complaintId) =>
                await _context.Complaints
                    .Include(c => c.Citizen)
                        .ThenInclude(c => c.User)
                    .Include(c => c.ComplaintType)
                    .Include(c => c.ComplaintDocuments)
                    .FirstOrDefaultAsync(c => c.AgentId == agentId && c.Id == complaintId);

            public async Task<Complaint> UpdateComplaintStatusAsync(Complaint complaint)
            {
                _context.Complaints.Update(complaint);
                await _context.SaveChangesAsync();
                return complaint;
            }

            public async Task<Complaint> UploadSolutionDocumentAsync(int complaintId, ComplaintDocument document)
            {
                var complaint = await _context.Complaints
                    .Include(c => c.ComplaintDocuments)
                    .FirstOrDefaultAsync(c => c.Id == complaintId);

                if (complaint == null) throw new Exception("Complaint not found");

                complaint.ComplaintDocuments ??= new List<ComplaintDocument>();
                complaint.ComplaintDocuments.Add(document);

                complaint.Status = ComplaintStatus.Resolved;
                complaint.UpdatedAt = DateTime.UtcNow;

                _context.Complaints.Update(complaint);
                await _context.SaveChangesAsync();

                return complaint;
            }

            public async Task<IEnumerable<Complaint>> SearchComplaintsAsync(int agentId, int? typeId, DateTime? from, DateTime? to, string citizenName)
            {
                var query = _context.Complaints
                    .Include(c => c.Citizen)
                        .ThenInclude(c => c.User)
                    .Include(c => c.ComplaintType)
                    .Include(c => c.ComplaintDocuments)
                    .Where(c => c.AgentId == agentId)
                    .AsQueryable();

                if (typeId.HasValue) query = query.Where(c => c.ComplaintTypeId == typeId.Value);
                if (from.HasValue) query = query.Where(c => c.CreatedAt >= from.Value);
                if (to.HasValue) query = query.Where(c => c.CreatedAt <= to.Value);
                if (!string.IsNullOrWhiteSpace(citizenName)) query = query.Where(c => c.Citizen.User.Name.Contains(citizenName));

                return await query.ToListAsync();
            }
        }
    }

