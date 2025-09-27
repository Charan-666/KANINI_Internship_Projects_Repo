using Complaint_2._0.Models;

namespace Complaint_2._0.Services
{
    public interface IComplaintTypeService
    {
        Task<IEnumerable<ComplaintType>> GetAllAsync();
        Task<ComplaintType> GetByIdAsync(int id);
        Task<ComplaintType> CreateAsync(string typeName);
        Task<ComplaintType> UpdateAsync(int id, string newName);
        Task<bool> DeleteAsync(int id);
    }
}
