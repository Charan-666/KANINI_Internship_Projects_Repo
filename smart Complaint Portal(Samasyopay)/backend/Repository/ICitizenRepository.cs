using Complaint_2._0.Models;

namespace Complaint_2._0.Repository
{
    public interface ICitizenRepository
    {
        Task<Citizen> GetProfileAsync(int citizenId);
        Task<Citizen> UpdateProfileAsync(Citizen citizen);

        Task<Complaint> CreateComplaintAsync(Complaint complaint);
        Task<IEnumerable<Complaint>> GetMyComplaintsAsync(int citizenId);
        Task<Complaint> GetComplaintDetailsAsync(int citizenId, int complaintId);
        Task<Complaint> UpdateComplaintAsync(Complaint complaint);
        Task<bool> DeleteComplaintAsync(int citizenId, int complaintId);
        Task<int?> GetAgentByComplaintTypeAsync(int complaintTypeId);
    }
}
