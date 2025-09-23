using Microsoft.EntityFrameworkCore;
using Smart_Complaint_Registration.Data;
using Smart_Complaint_Registration.Dto;
using Smart_Complaint_Registration.Interfaces;
using Smart_Complaint_Registration.Models;
using System;

namespace Smart_Complaint_Registration.Services
{
    public class UserService : IUserService
    {
        private readonly SmartDbContext _context;

        public UserService(SmartDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUserAsync(User user, int? departmentId = null)
        {
            // Case 1: If user is Citizen or Admin
            if (user.Role == User.UserRole.Citizen || user.Role == User.UserRole.Admin)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }

            // Case 2: If user is Head or Agent -> also create DepartmentUser
            if (user.Role == User.UserRole.Head || user.Role == User.UserRole.Agent)
            {
                if (!departmentId.HasValue)
                    throw new ArgumentException("DepartmentId is required for Head or Agent users.");

                var departmentUser = new DepartmentUser
                {
                    Name = user.Username,
                    Email = user.Email,
                    Phone = "N/A", // default, can be updated later
                    Role = user.Role.ToString(),
                    DepartmentId = departmentId.Value
                };

                _context.DepartmentUsers.Add(departmentUser);
                await _context.SaveChangesAsync();

                // Link User to DepartmentUser
                user.DepartmentUserId = departmentUser.DepartmentUserId;
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return user;
            }

            throw new ArgumentException("Invalid role selected.");
        }

        public async Task<CitizenResponseDto> RegisterCitizenAsync(RegisterCitizenDto dto)
        {

            byte[]? photoBytes = null;

            if (dto.ProfilePhoto != null && dto.ProfilePhoto.Length > 0)
            {
                using var ms = new MemoryStream();
                await dto.ProfilePhoto.CopyToAsync(ms);
                photoBytes = ms.ToArray();
            }

            // Step 1: Create Citizen
            var citizen = new Citizen
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address,
                AadhaarNumber = dto.AadhaarNumber,
                ProfilePhoto = photoBytes
            };

            _context.Citizens.Add(citizen);
            await _context.SaveChangesAsync();

            // Step 2: Create linked User
            var user = new User
            {
                Username = dto.Email,
                Email = dto.Email,
                PasswordHash = dto.Password, 
                Role = User.UserRole.Citizen,
                CitizenId = citizen.CitizenId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new CitizenResponseDto
            {
                CitizenId = citizen.CitizenId,
                Name = citizen.Name,
                Email = citizen.Email,
                ProfilePhotoBase64 = citizen.ProfilePhoto != null
        ? Convert.ToBase64String(citizen.ProfilePhoto)
        : null
            };
        }

        public async Task<User?> LoginAsync(LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u =>
                    u.Username == dto.UsernameOrEmail || u.Email == dto.UsernameOrEmail);

            if (user == null) return null;

            // ⚠️ For now plain check; later hash compare
            if (user.PasswordHash != dto.Password) return null;

            user.LastLoginAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return user;
        }

    }
}
