using Microsoft.EntityFrameworkCore;
using System;
using WebApiFinalProject.Data;
using WebApiFinalProject.Interfaces;
using WebApiFinalProject.Models;

namespace WebApiFinalProject.Repository

{
    public class OwnerRepository : IOwnerVehicleFeature<Owner>, IOwnerRepository
    {
        private readonly ApplicaionDbContext _context;
        private readonly DbSet<Owner> _dbSet;

        public OwnerRepository(ApplicaionDbContext context) 
        {
            _context = context;
            _dbSet = _context.Set<Owner>();
        }

        public async Task<IEnumerable<Owner>> GetAllAsync()
        {
            return await _context.Owners
                .Include(o => o.Vehicles)
                .ToListAsync();
        }

        public async Task<Owner> GetByIdAsync(int id)
        {
            return await _context.Owners
                .Include(o => o.Vehicles)
                .FirstOrDefaultAsync(o => o.OwnerId == id);
        }

        public async Task<Owner> AddAsync(Owner entity)
            
        {
             _context.Owners.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Owner> UpdateAsync(Owner entity)
        {
            _context.Owners.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var owner = await _context.Owners.FindAsync(id);
            if (owner == null) return false;
            
            
                _context.Owners.Remove(owner);
                await _context.SaveChangesAsync();
            return true;
          
        }


        //addtional operation
        public IEnumerable<Owner> SearchByName(string name)
        {
            return _dbSet.AsNoTracking()
                    .Where(o => o.Name.Contains(name))
                    .ToList();
        }

        public IEnumerable<Owner> Filter(Func<Owner, bool> predicate)
        {
            return _dbSet.AsNoTracking().ToList().Where(predicate).ToList();
        }
        

        public int CountOwners()
        {
            return _dbSet.Count();
        }

        public int CountOwners(Func<Owner, bool> predicate)
        {
            return _dbSet.AsNoTracking().ToList().Count(predicate);
        }



   



    }




}



