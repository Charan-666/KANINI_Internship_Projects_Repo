using WebApiFinalProject.Models;

namespace WebApiFinalProject.Interfaces
{
    public interface IFeatureRepository : IOwnerVehicleFeature<Feature>
    {
        IEnumerable<Feature> SearchByName(string name);
        IEnumerable<Feature> Filter(Func<Feature, bool> predicate);
        int CountFeatures();
        int CountFeatures(Func<Feature, bool> predicate);
    }
}
