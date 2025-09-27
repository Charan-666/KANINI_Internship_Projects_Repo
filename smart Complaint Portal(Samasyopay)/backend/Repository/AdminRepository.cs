using Complaint_2._0.data;
using Complaint_2._0.Models;
using Microsoft.EntityFrameworkCore;

namespace Complaint_2._0.Repository
{
  
        public class AdminRepository : IAdminRepository
        {
            private readonly AppDbContext _context;

            public AdminRepository(AppDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<User>> GetAllUsersAsync(UserRole? role = null)
            {
                var query = _context.Users.Include(u => u.AgentProfile).Include(u => u.CitizenProfile).AsQueryable();
                if (role.HasValue) query = query.Where(u => u.Role == role.Value);
                return await query.ToListAsync();
            }

            public async Task<User> AddAgentAsync(User agentUser, Agent agentProfile)
            {
                _context.Users.Add(agentUser);
                await _context.SaveChangesAsync();

                agentProfile.UserId = agentUser.Id;
                _context.Agents.Add(agentProfile);
                await _context.SaveChangesAsync();

                return agentUser;
            }

            public async Task<User> UpdateAgentAsync(int agentId, User updatedUser, Agent updatedProfile)
            {
                var user = await _context.Users.Include(u => u.AgentProfile).FirstOrDefaultAsync(u => u.Id == agentId && u.Role == UserRole.Agent);
                if (user == null) throw new Exception("Agent not found");

                user.Name = updatedUser.Name;
                user.Email = updatedUser.Email;
                // Update password if required
                if (!string.IsNullOrEmpty(updatedUser.PasswordHash)) user.PasswordHash = updatedUser.PasswordHash;

                user.AgentProfile.Department = updatedProfile.Department;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return user;
            }

            public async Task<bool> DeleteAgentAsync(int agentId)
            {
                var agent = await _context.Users.Include(u => u.AgentProfile).FirstOrDefaultAsync(u => u.Id == agentId && u.Role == UserRole.Agent);
                if (agent == null) return false;

                // Reassign complaints (optional: set AgentId = null)
                var complaints = await _context.Complaints.Where(c => c.AgentId == agentId).ToListAsync();
                foreach (var c in complaints) c.AgentId = null;

                _context.Complaints.UpdateRange(complaints);
                _context.Users.Remove(agent);
                await _context.SaveChangesAsync();
                return true;
            }

            public async Task<bool> DeleteUserAsync(int userId)
            {
                var user = await _context.Users.Include(u => u.AgentProfile).Include(u => u.CitizenProfile).FirstOrDefaultAsync(u => u.Id == userId);
                if (user == null) return false;

                // If agent, unassign complaints
                if (user.Role == UserRole.Agent)
                {
                    var complaints = await _context.Complaints.Where(c => c.AgentId == userId).ToListAsync();
                    foreach (var c in complaints) c.AgentId = null;
                    _context.Complaints.UpdateRange(complaints);
                }
                // If citizen, delete their complaints
                else if (user.Role == UserRole.Citizen)
                {
                    var complaints = await _context.Complaints.Where(c => c.CitizenId == userId).ToListAsync();
                    _context.Complaints.RemoveRange(complaints);
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }

            public async Task<IEnumerable<Complaint>> GetAllComplaintsAsync()
            {
                return await _context.Complaints
                    .Include(c => c.Citizen)
                    .Include(c => c.Agent)
                    .Include(c => c.ComplaintType)
                    .Include(c => c.ComplaintDocuments)
                    .ToListAsync();
            }

            public async Task<Complaint> GetComplaintDetailsAsync(int complaintId)
            {
                return await _context.Complaints
                    .Include(c => c.Citizen)
                    .Include(c => c.Agent)
                    .Include(c => c.ComplaintType)
                    .Include(c => c.ComplaintDocuments)
                    .FirstOrDefaultAsync(c => c.Id == complaintId);
            }

            public async Task<Complaint> AssignComplaintAsync(int complaintId, int agentId)
            {
                var complaint = await _context.Complaints.FirstOrDefaultAsync(c => c.Id == complaintId);
                if (complaint == null) throw new Exception("Complaint not found");

                complaint.AgentId = agentId;
                complaint.Status = ComplaintStatus.Assigned;
                complaint.UpdatedAt = DateTime.UtcNow;

                _context.Complaints.Update(complaint);
                await _context.SaveChangesAsync();
                return complaint;
            }

            public async Task<IEnumerable<Complaint>> SearchComplaintsAsync(int? typeId, DateTime? from, DateTime? to, ComplaintStatus? status, int? agentId, int? citizenId)
            {
                var query = _context.Complaints
                    .Include(c => c.Citizen)
                    .Include(c => c.Agent)
                    .Include(c => c.ComplaintType)
                    .Include(c => c.ComplaintDocuments)
                    .AsQueryable();

                if (typeId.HasValue) query = query.Where(c => c.ComplaintTypeId == typeId.Value);
                if (from.HasValue) query = query.Where(c => c.CreatedAt >= from.Value);
                if (to.HasValue) query = query.Where(c => c.CreatedAt <= to.Value);
                if (status.HasValue) query = query.Where(c => c.Status == status.Value);
                if (agentId.HasValue) query = query.Where(c => c.AgentId == agentId.Value);
                if (citizenId.HasValue) query = query.Where(c => c.CitizenId == citizenId.Value);

                return await query.ToListAsync();
            }

            // Complaint Type Management
            public async Task<IEnumerable<ComplaintType>> GetAllComplaintTypesAsync() =>
                await _context.ComplaintTypes.ToListAsync();

            public async Task<ComplaintType> CreateComplaintTypeAsync(string name, string typeName)
            {
                var type = new ComplaintType { Name = name, TypeName = typeName };
                _context.ComplaintTypes.Add(type);
                await _context.SaveChangesAsync();
                return type;
            }

            public async Task<ComplaintType> UpdateComplaintTypeAsync(int id, string name, string typeName)
            {
                var type = await _context.ComplaintTypes.FindAsync(id);
                if (type == null) return null;

                type.Name = name;
                type.TypeName = typeName;
                _context.ComplaintTypes.Update(type);
                await _context.SaveChangesAsync();
                return type;
            }

            public async Task<bool> DeleteComplaintTypeAsync(int id)
            {
                var type = await _context.ComplaintTypes.FindAsync(id);
                if (type == null) return false;

                // Delete all complaints of this type first
                var complaints = await _context.Complaints.Where(c => c.ComplaintTypeId == id).ToListAsync();
                _context.Complaints.RemoveRange(complaints);

                _context.ComplaintTypes.Remove(type);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }

