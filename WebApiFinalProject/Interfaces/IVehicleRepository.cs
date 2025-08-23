using WebApiFinalProject.Models;

namespace WebApiFinalProject.Interfaces
{
    public interface IVehicleRepository : IOwnerVehicleFeature<Vehicle>
    {
        IEnumerable<Vehicle> SearchByModel(string model);
        IEnumerable<Vehicle> Filter(Func<Vehicle, bool> predicate);
        int CountVehicles();
        int CountVehicles(Func<Vehicle, bool> predicate);

        IEnumerable<Vehicle> GetVehiclesByDate(DateOnly date);
    }
}
