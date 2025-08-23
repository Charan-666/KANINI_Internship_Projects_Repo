using WebApiFinalProject.Interfaces;
using WebApiFinalProject.Models;
using WebApiFinalProject.Repository;

namespace WebApiFinalProject.Services
{
    public class VehicleService
    {
        private readonly IOwnerVehicleFeature<Vehicle> _vehicleRepo;
        private readonly IVehicleRepository _vehicleonly;

        public VehicleService(IOwnerVehicleFeature<Vehicle> vehicleRepo, IVehicleRepository vehicleonly)
        {
            _vehicleRepo = vehicleRepo;
            _vehicleonly = vehicleonly;
        }
      

        public async Task<IEnumerable<Vehicle>> GetAllAsync()
        {
            return await _vehicleRepo.GetAllAsync();
        }

        public async Task<Vehicle?> GetByIdAsync(int id)
        {
            return await _vehicleRepo.GetByIdAsync(id);
        }

        public async Task<Vehicle> AddAsync(Vehicle vehicle)
        {
            return await _vehicleRepo.AddAsync(vehicle);
        }

        public async Task<Vehicle> UpdateAsync(Vehicle vehicle)
        {
            return await _vehicleRepo.UpdateAsync(vehicle);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _vehicleRepo.DeleteAsync(id);
        }

        //additional

        public IEnumerable<Vehicle> SearchVehicles(string model) => _vehicleonly.SearchByModel(model);
        public IEnumerable<Vehicle> FilterVehicles(Func<Vehicle, bool> predicate) => _vehicleonly.Filter(predicate);
        public int GetVehicleCount() => _vehicleonly.CountVehicles();
        public int GetConditionalVehicleCount(Func<Vehicle, bool> predicate) => _vehicleonly.CountVehicles(predicate);


        public IEnumerable<Vehicle> GetVehiclesByDate(DateOnly date)
        {
            return _vehicleonly.GetVehiclesByDate(date);
        }
    }
}
