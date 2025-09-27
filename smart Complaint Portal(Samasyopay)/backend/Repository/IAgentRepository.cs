using Complaint_2._0.Models;

namespace Complaint_2._0.Repository
{
    public interface IAgentRepository
    {
        Task<IEnumerable<Complaint>> GetAssignedComplaintsAsync(int agentId);
        Task<Complaint> GetComplaintDetailsAsync(int agentId, int complaintId);
        Task<Complaint> UpdateComplaintStatusAsync(Complaint complaint);
        Task<Complaint> UploadSolutionDocumentAsync(int complaintId, ComplaintDocument document);
        Task<IEnumerable<Complaint>> SearchComplaintsAsync(int agentId, int? typeId, DateTime? from, DateTime? to, string citizenName);

    }
}
