using Complaint_2._0.dto;
using Complaint_2._0.Models;

namespace Complaint_2._0.Services
{
    public interface IAgentService
    {
        Task<IEnumerable<Complaint>> GetAssignedComplaintsAsync(int agentId);
        Task<Complaint> GetComplaintDetailsAsync(int agentId, int complaintId);
        Task<Complaint> UpdateComplaintStatusAsync(int agentId, int complaintId, ComplaintStatus status);
        Task<Complaint> UploadSolutionDocumentAsync(int agentId, int complaintId, UploadDto solutionFile);
        Task<IEnumerable<Complaint>> SearchComplaintsAsync(int agentId, int? typeId, DateTime? from, DateTime? to, string citizenName);

    }
}
