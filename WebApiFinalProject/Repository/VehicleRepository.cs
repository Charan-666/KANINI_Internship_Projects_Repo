using Microsoft.EntityFrameworkCore;
using System;
using WebApiFinalProject.Data;
using WebApiFinalProject.Interfaces;
using WebApiFinalProject.Models;

namespace WebApiFinalProject.Repository
{
    public class VehicleRepository : IOwnerVehicleFeature<Vehicle> , IVehicleRepository
    {
        private readonly ApplicaionDbContext _context;
        private readonly DbSet<Vehicle> _dbSet;

        public VehicleRepository(ApplicaionDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Vehicle>();

        }

        public async Task<IEnumerable<Vehicle>> GetAllAsync()
        {
            return await _context.Vehicles
                .Include(v => v.Owner)
                .Include(v => v.Features)
                .ToListAsync();
        }

        public async Task<Vehicle> GetByIdAsync(int id)
        {
            return await _context.Vehicles
                .Include(v => v.Owner)
                .Include(v => v.Features)
                .FirstOrDefaultAsync(v => v.VehicleId == id);
        }

        public async Task<Vehicle> AddAsync(Vehicle entity)

        {
            _context.Vehicles.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Vehicle> UpdateAsync(Vehicle entity)
        {
            _context.Vehicles.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null) return false;


            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
            return true;

        }

        //addtional
        public IEnumerable<Vehicle> SearchByModel(string model)
        {
            return _dbSet.AsNoTracking()
                         .Where(v => v.Model.Contains(model))
                         .ToList();
        }

        public IEnumerable<Vehicle> Filter(Func<Vehicle, bool> predicate)
        {
            return _dbSet.AsNoTracking().ToList().Where(predicate).ToList();
        }

        public int CountVehicles()
        {
            return _dbSet.Count();
        }

        public int CountVehicles(Func<Vehicle, bool> predicate)
        {
            return _dbSet.AsNoTracking().ToList().Count(predicate);
        }

        public IEnumerable<Vehicle> GetVehiclesByDate(DateOnly date)
        {
            return _context.Vehicles
                .AsNoTracking()
                .Where(v => v.date == date)
                .ToList();
        }
    }
}
