using Authorizationwebapi.Interfaces;
using Authorizationwebapis.Models;

namespace Authorizationwebapi.Services
{
    public class LockerService
    {
        private readonly ILockerRepository _lockerRepository;

        public LockerService(ILockerRepository lockerRepository)
        {
            _lockerRepository = lockerRepository;
        }

        public async Task<IEnumerable<Locker>> GetAllLockersAsync()
        {
            return await _lockerRepository.GetAllLockersAsync();
        }

        public async Task<Locker> GetLockerByIdAsync(int id)
        {
            return await _lockerRepository.GetLockerByIdAsync(id);
        }

        public async Task AddLockerAsync(Locker locker)
        {
            await _lockerRepository.AddLockerAsync(locker);
        }

        public async Task UpdateLockerAsync(Locker locker)
        {
            await _lockerRepository.UpdateLockerAsync(locker);
        }

        public async Task DeleteLockerAsync(int id)
        {
            await _lockerRepository.DeleteLockerAsync(id);
        }

        public async Task<bool> LockerExistsAsync(int id)
        {
            return await _lockerRepository.LockerExistsAsync(id);
        }
    }
}
