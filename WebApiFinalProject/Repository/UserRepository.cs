using Microsoft.EntityFrameworkCore;
using System;
using WebApiFinalProject.Data;
using WebApiFinalProject.Interfaces;
using WebApiFinalProject.Models;

namespace WebApiFinalProject.Repository
{
    public class UserRepository : IOwnerVehicleFeature<User>, IUser
    {
        private readonly ApplicaionDbContext _context;

        public UserRepository(ApplicaionDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.Role)
                .ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users.Include(r => r.Role).FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> AddAsync(User entity)

        {
            _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<User> UpdateAsync(User entity)
        {
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;


            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}
