using Smart_Complaint_Registration.Interfaces;
using Smart_Complaint_Registration.Models;

namespace Smart_Complaint_Registration.Services
{
    public class DepartmentService
    {
        private readonly IDepartmentRepository _repo;

        public DepartmentService(IDepartmentRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Department>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<Department?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);
        public async Task<Department> AddAsync(Department entity) => await _repo.AddAsync(entity);
        public async Task<Department> UpdateAsync(Department entity) => await _repo.UpdateAsync(entity);
        public async Task<bool> DeleteAsync(int id) => await _repo.DeleteAsync(id);
    }
}
