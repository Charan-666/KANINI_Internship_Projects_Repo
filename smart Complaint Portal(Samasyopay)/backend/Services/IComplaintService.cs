using Complaint_2._0.dto;
using Complaint_2._0.Models;

namespace Complaint_2._0.Services
{
    public interface IComplaintService
    {
        Task<Complaint> CreateComplaintAsync(int citizenId, CreateComplaintDTO dto);
        Task<List<Complaint>> GetComplaintsByCitizenAsync(int citizenId);
        Task<List<Complaint>> GetComplaintsByAgentAsync(int agentId);
        Task<List<Complaint>> GetAllComplaintsAsync();
        Task<Complaint> GetComplaintByIdAsync(int id);
        Task<Complaint> UpdateComplaintStatusAsync(UpdateComplaintStatusDTO dto);
        Task<Complaint> AssignComplaintAsync(AssignComplaintDTO dto);
        Task DeleteComplaintAsync(int complaintId);
    }
}
