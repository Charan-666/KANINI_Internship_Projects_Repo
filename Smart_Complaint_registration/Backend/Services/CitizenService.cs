using Microsoft.EntityFrameworkCore;
using Smart_Complaint_Registration.Interfaces;
using Smart_Complaint_Registration.Models;

namespace Smart_Complaint_Registration.Services
{
    public class CitizenService
    {
        private readonly ICitizenRepository _repo;

        public CitizenService(ICitizenRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Citizen>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<Citizen?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);
        public async Task<Citizen> AddAsync(Citizen entity) => await _repo.AddAsync(entity);
        public async Task<Citizen> UpdateAsync(Citizen entity) => await _repo.UpdateAsync(entity);
        public async Task<bool> DeleteAsync(int id) => await _repo.DeleteAsync(id);

        
    }
}
