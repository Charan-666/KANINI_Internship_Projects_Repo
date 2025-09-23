using Smart_Complaint_Registration.Models;

namespace Smart_Complaint_Registration.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
