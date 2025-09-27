using Complaint_2._0.Models;
using Complaint_2._0.Repository;

namespace Complaint_2._0.Services
{
    public class ComplaintTypeService : IComplaintTypeService
    {
        private readonly IComplaintTypeRepository _repo;
        public ComplaintTypeService(IComplaintTypeRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ComplaintType>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<ComplaintType> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<ComplaintType> CreateAsync(string typeName)
        {
            var type = new ComplaintType { TypeName = typeName };
            await _repo.AddAsync(type);
            await _repo.SaveAsync();
            return type;
        }

        public async Task<ComplaintType> UpdateAsync(int id, string newName)
        {
            var type = await _repo.GetByIdAsync(id);
            if (type == null) return null;

            type.TypeName = newName;
            await _repo.UpdateAsync(type);
            await _repo.SaveAsync();
            return type;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var type = await _repo.GetByIdAsync(id);
            if (type == null) return false;

            await _repo.DeleteAsync(type);
            await _repo.SaveAsync();
            return true;
        }
    }
}
