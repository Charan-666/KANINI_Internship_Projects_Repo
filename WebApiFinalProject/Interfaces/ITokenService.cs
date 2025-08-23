using WebApiFinalProject.Models;

namespace WebApiFinalProject.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
