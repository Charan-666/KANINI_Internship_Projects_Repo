using Smart_Complaint_Registration.Dto;
using Smart_Complaint_Registration.Models;

namespace Smart_Complaint_Registration.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(User user, int? departmentId = null);
        Task<CitizenResponseDto> RegisterCitizenAsync(RegisterCitizenDto dto);
        Task<User?> LoginAsync(LoginDto dto);

    }
}
