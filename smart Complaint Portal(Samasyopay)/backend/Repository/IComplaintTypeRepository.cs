using Complaint_2._0.Models;

namespace Complaint_2._0.Repository
{
    public interface IComplaintTypeRepository
    {
        Task<IEnumerable<ComplaintType>> GetAllAsync();
        Task<ComplaintType> GetByIdAsync(int id);
        Task AddAsync(ComplaintType type);
        Task UpdateAsync(ComplaintType type);
        Task DeleteAsync(ComplaintType type);
        Task SaveAsync();
    }
}
