using Microsoft.EntityFrameworkCore;
using System;
using WebApiFinalProject.Data;
using WebApiFinalProject.Interfaces;
using WebApiFinalProject.Models;

namespace WebApiFinalProject.Repository
{
    public class FeatureRepository : IOwnerVehicleFeature<Feature>, IFeatureRepository
    {
        private readonly ApplicaionDbContext _context;
        private readonly DbSet<Feature> _dbSet;

        public FeatureRepository(ApplicaionDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Feature>();

        }

        public async Task<IEnumerable<Feature>> GetAllAsync()
        {
            return await _context.Features
                .Include(f => f.Vehicle)
                .ToListAsync();
        }

        public async Task<Feature> GetByIdAsync(int id)
        {
            return await _context.Features
                .Include(f => f.Vehicle)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Feature> AddAsync(Feature entity)

        {
            _context.Features.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Feature> UpdateAsync(Feature entity)
        {
            _context.Features.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var feature = await _context.Features.FindAsync(id);
            if (feature == null) return false;


            _context.Features.Remove(feature);
            await _context.SaveChangesAsync();
            return true;

        }

        //additional
        public IEnumerable<Feature> SearchByName(string name)
        {
            return _dbSet.AsNoTracking()
                         .Where(f => f.Name.Contains(name))
                         .ToList();
        }

        public IEnumerable<Feature> Filter(Func<Feature, bool> predicate)
        {
            return _dbSet.AsNoTracking().ToList().Where(predicate).ToList();
        }

        public int CountFeatures()
        {
            return _dbSet.Count();
        }

        public int CountFeatures(Func<Feature, bool> predicate)
        {
            return _dbSet.AsNoTracking().ToList().Count(predicate);
        }
    }
}
