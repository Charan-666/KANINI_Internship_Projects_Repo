using WebApiFinalProject.Interfaces;
using WebApiFinalProject.Models;


namespace WebApiFinalProject.Services
{
    public class OwnerService
    {
        private readonly IOwnerVehicleFeature<Owner> _ownerRepo;
        private readonly IOwnerRepository _owneronly;

        public OwnerService(IOwnerVehicleFeature<Owner> ownerRepo, IOwnerRepository owneronly)
        {
            _ownerRepo = ownerRepo;
            _owneronly = owneronly;
        }

        public async Task<IEnumerable<Owner>> GetAllAsync()
        {
            return await _ownerRepo.GetAllAsync();
        }

        public async Task<Owner?> GetByIdAsync(int id)
        {
            return await _ownerRepo.GetByIdAsync(id);
        }

        public async Task<Owner> AddAsync(Owner owner)
        {
            return await _ownerRepo.AddAsync(owner);
        }

        public async Task<Owner> UpdateAsync(Owner owner)
        {
            return await _ownerRepo.UpdateAsync(owner);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _ownerRepo.DeleteAsync(id);
        }

        //search,filter,count using linq,delegate 
        public IEnumerable<Owner> SearchOwners(string name) => _owneronly.SearchByName(name);
        public IEnumerable<Owner> FilterOwners(Func<Owner, bool> predicate) => _owneronly.Filter(predicate);
        public int GetOwnerCount() => _owneronly.CountOwners();
        public int GetConditionalOwnerCount(Func<Owner, bool> predicate) => _owneronly.CountOwners(predicate);
    }
}
