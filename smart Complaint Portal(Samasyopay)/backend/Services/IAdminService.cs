using Complaint_2._0.Models;

namespace Complaint_2._0.Services
{
    public interface IAdminService
    {
        Task<IEnumerable<User>> GetAllUsersAsync(UserRole? role = null);
        Task<User> AddAgentAsync(User agentUser, Agent agentProfile);
        Task<User> UpdateAgentAsync(int agentId, User updatedUser, Agent updatedProfile);
        Task<bool> DeleteAgentAsync(int agentId);
        Task<bool> DeleteUserAsync(int userId);

        Task<IEnumerable<Complaint>> GetAllComplaintsAsync();
        Task<Complaint> GetComplaintDetailsAsync(int complaintId);
        Task<Complaint> AssignComplaintAsync(int complaintId, int agentId);
        Task<IEnumerable<Complaint>> SearchComplaintsAsync(int? typeId, DateTime? from, DateTime? to, ComplaintStatus? status, int? agentId, int? citizenId);

        // Complaint Type Management
        Task<IEnumerable<ComplaintType>> GetAllComplaintTypesAsync();
        Task<ComplaintType> CreateComplaintTypeAsync(string name, string typeName);
        Task<ComplaintType> UpdateComplaintTypeAsync(int id, string name, string typeName);
        Task<bool> DeleteComplaintTypeAsync(int id);

    }
}
