using Complaint_2._0.Models;

namespace Complaint_2._0.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
