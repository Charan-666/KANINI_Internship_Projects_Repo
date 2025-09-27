using Complaint_2._0.dto;
using Complaint_2._0.Models;

namespace Complaint_2._0.Services
{
    public interface ICitizenService
    {
        Task<Citizen> GetProfileAsync(int citizenId);
        Task<Citizen> UpdateProfileAsync(int citizenId, UpdateCitizenDTO dto);

        Task<Complaint> RaiseComplaintAsync(int citizenId, CreateComplaintDTO dto);
        Task<IEnumerable<Complaint>> GetMyComplaintsAsync(int citizenId);
        Task<Complaint> GetComplaintDetailsAsync(int citizenId, int complaintId);
        Task<Complaint> UpdateComplaintAsync(int citizenId, UpdateComplaintDTO dto);
        Task<bool> DeleteComplaintAsync(int citizenId, int complaintId);
    }
}
