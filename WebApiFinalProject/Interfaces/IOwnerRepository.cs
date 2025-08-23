using WebApiFinalProject.Models;

namespace WebApiFinalProject.Interfaces
{
    public interface IOwnerRepository : IOwnerVehicleFeature<Owner>
    {
        
            IEnumerable<Owner> SearchByName(string name);
            IEnumerable<Owner> Filter(Func<Owner, bool> predicate);
            int CountOwners();
            int CountOwners(Func<Owner, bool> predicate);
        
    }
}
