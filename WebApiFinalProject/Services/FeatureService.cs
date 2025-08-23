using WebApiFinalProject.Interfaces;
using WebApiFinalProject.Models;

namespace WebApiFinalProject.Services
{
    public class FeatureService
    {
        private readonly IOwnerVehicleFeature<Feature> _featureRepo;
        private readonly IFeatureRepository _featureonly;

        public FeatureService(IOwnerVehicleFeature<Feature> featureRepo, IFeatureRepository featureonly)
        {
            _featureRepo = featureRepo;
            _featureonly = featureonly;
        }
       

        public async Task<IEnumerable<Feature>> GetAllAsync()
        {
            return await _featureRepo.GetAllAsync();
        }

        public async Task<Feature?> GetByIdAsync(int id)
        {
            return await _featureRepo.GetByIdAsync(id);
        }

        public async Task<Feature> AddAsync(Feature feature)
        {
            return await _featureRepo.AddAsync(feature);
        }

        public async Task<Feature> UpdateAsync(Feature feature)
        {
            return await _featureRepo.UpdateAsync(feature);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _featureRepo.DeleteAsync(id);
        }

        //additional
        public IEnumerable<Feature> SearchFeatures(string name) => _featureonly.SearchByName(name);
        public IEnumerable<Feature> FilterFeatures(Func<Feature, bool> predicate) => _featureonly.Filter(predicate);
        public int GetFeatureCount() => _featureonly.CountFeatures();
        public int GetConditionalFeatureCount(Func<Feature, bool> predicate) => _featureonly.CountFeatures(predicate);
    }
}
