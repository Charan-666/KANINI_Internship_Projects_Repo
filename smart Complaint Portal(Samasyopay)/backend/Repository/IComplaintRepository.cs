using Complaint_2._0.Models;

namespace Complaint_2._0.Repository
{
    public interface IComplaintRepository
    {
        Task<Complaint> AddAsync(Complaint complaint);
        Task<Complaint> GetByIdAsync(int id);
        Task<List<Complaint>> GetByCitizenIdAsync(int citizenId);
        Task<List<Complaint>> GetByAgentIdAsync(int agentId);
        Task<List<Complaint>> GetAllAsync();
        Task<Complaint> UpdateAsync(Complaint complaint);
        Task DeleteAsync(Complaint complaint);
    }
}
