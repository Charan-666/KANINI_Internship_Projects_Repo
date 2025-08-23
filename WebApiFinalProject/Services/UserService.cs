using WebApiFinalProject.Interfaces;
using WebApiFinalProject.Models;

namespace WebApiFinalProject.Services
{
    public class UserService
    {
        private readonly IOwnerVehicleFeature<User> _userRepo;
        private readonly IUser _user;

        public UserService(IOwnerVehicleFeature<User> userRepo, IUser user)
        {
            _userRepo = userRepo;
            _user = user;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepo.GetAllAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _userRepo.GetByIdAsync(id);
        }

        public async Task<User> GetByNameAsync(string name)
        {
            return await _user.GetByUsernameAsync(name);
        }

        public async Task<User> AddAsync(User user)
        {
            return await _userRepo.AddAsync(user);
        }

        public async Task<User> UpdateAsync(User user)
        {
            return await _userRepo.UpdateAsync(user);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _userRepo.DeleteAsync(id);
        }
    }
}
