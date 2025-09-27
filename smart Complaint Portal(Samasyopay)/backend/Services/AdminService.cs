using Complaint_2._0.Models;
using Complaint_2._0.Repository;
using BCrypt.Net;

namespace Complaint_2._0.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _repo;

        public AdminService(IAdminRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<User>> GetAllUsersAsync(UserRole? role = null) =>
            _repo.GetAllUsersAsync(role);

        public Task<User> AddAgentAsync(User agentUser, Agent agentProfile)
        {
            agentUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(agentUser.PasswordHash);
            return _repo.AddAgentAsync(agentUser, agentProfile);
        }

        public Task<User> UpdateAgentAsync(int agentId, User updatedUser, Agent updatedProfile) =>
            _repo.UpdateAgentAsync(agentId, updatedUser, updatedProfile);

        public Task<bool> DeleteAgentAsync(int agentId) =>
            _repo.DeleteAgentAsync(agentId);

        public Task<bool> DeleteUserAsync(int userId) =>
            _repo.DeleteUserAsync(userId);

        public Task<IEnumerable<Complaint>> GetAllComplaintsAsync() =>
            _repo.GetAllComplaintsAsync();

        public Task<Complaint> GetComplaintDetailsAsync(int complaintId) =>
            _repo.GetComplaintDetailsAsync(complaintId);

        public Task<Complaint> AssignComplaintAsync(int complaintId, int agentId) =>
            _repo.AssignComplaintAsync(complaintId, agentId);

        public Task<IEnumerable<Complaint>> SearchComplaintsAsync(int? typeId, DateTime? from, DateTime? to, ComplaintStatus? status, int? agentId, int? citizenId) =>
            _repo.SearchComplaintsAsync(typeId, from, to, status, agentId, citizenId);

        // Complaint Type Management
        public Task<IEnumerable<ComplaintType>> GetAllComplaintTypesAsync() =>
            _repo.GetAllComplaintTypesAsync();

        public Task<ComplaintType> CreateComplaintTypeAsync(string name, string typeName) =>
            _repo.CreateComplaintTypeAsync(name, typeName);

        public Task<ComplaintType> UpdateComplaintTypeAsync(int id, string name, string typeName) =>
            _repo.UpdateComplaintTypeAsync(id, name, typeName);

        public Task<bool> DeleteComplaintTypeAsync(int id) =>
            _repo.DeleteComplaintTypeAsync(id);
    }
}
