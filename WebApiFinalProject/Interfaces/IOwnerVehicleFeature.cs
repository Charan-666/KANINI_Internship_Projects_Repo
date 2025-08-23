using WebApiFinalProject.Models;

namespace WebApiFinalProject.Interfaces
{
    public interface IOwnerVehicleFeature<T> where T : class
    {
       
           


        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);

    }
    public interface IUser
    {
        Task<User> GetByUsernameAsync(string username);
    }
}
