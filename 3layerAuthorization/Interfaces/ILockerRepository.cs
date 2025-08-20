using Authorizationwebapis.Models;
namespace Authorizationwebapi.Interfaces
{
    public interface ILockerRepository
    {
        Task<IEnumerable<Locker>> GetAllLockersAsync();
        Task<Locker> GetLockerByIdAsync(int id);
        Task AddLockerAsync(Locker locker);
        Task UpdateLockerAsync(Locker locker);
        Task DeleteLockerAsync(int id);
        Task<bool> LockerExistsAsync(int id);
    }
}
