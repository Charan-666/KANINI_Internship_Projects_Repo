using Complaint_2._0.data;
using Complaint_2._0.Models;
using Complaint_2._0.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace Complaint_2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ITokenService _tokenService;

        public AuthController(AppDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null) return Unauthorized("Invalid email or password");

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash)) 
                return Unauthorized("Invalid email or password");

            var token = _tokenService.GenerateToken(user);
            return Ok(new { 
                Token = token, 
                Role = user.Role.ToString(),
                UserId = user.Id,
                UserName = user.Name
            });
        }

        [HttpPost("create-admin")]
        public async Task<IActionResult> CreateAdmin()
        {
            try
            {
                // Check if admin already exists
                var existingAdmin = await _context.Users.FirstOrDefaultAsync(u => u.Email == "admin@admin.com");
                if (existingAdmin != null)
                    return BadRequest("Admin user already exists");

                // Create admin user
                var adminUser = new User
                {
                    Name = "Admin",
                    Email = "admin@admin.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Role = UserRole.Admin
                };

                _context.Users.Add(adminUser);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Admin user created successfully. Email: admin@admin.com, Password: admin123" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Failed to create admin", Error = ex.Message });
            }
        }

        [HttpPost("register")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Register([FromForm] RegisterDTO dto)
        {
            try
            {
                // Check if user already exists
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
                if (existingUser != null)
                    return BadRequest("User with this email already exists");

                // Handle profile photo upload
                byte[]? photoData = null;
                if (dto.Photo != null && dto.Photo.Length > 0)
                {
                    // Validate file type
                    var allowedTypes = new[] { "image/jpeg", "image/jpg", "image/png", "image/gif" };
                    if (!allowedTypes.Contains(dto.Photo.ContentType.ToLower()))
                        return BadRequest("Only image files (JPEG, PNG, GIF) are allowed");

                    // Validate file size (max 5MB)
                    if (dto.Photo.Length > 5 * 1024 * 1024)
                        return BadRequest("File size cannot exceed 5MB");

                    using var memoryStream = new MemoryStream();
                    await dto.Photo.CopyToAsync(memoryStream);
                    photoData = memoryStream.ToArray();
                }

                // Create new user
                var user = new User
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                    Role = UserRole.Citizen,
                    Photo = photoData
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Create citizen profile
                var citizen = new Citizen
                {
                    UserId = user.Id,
                    Address = dto.Address ?? "",
                    PhoneNumber = dto.PhoneNumber ?? ""
                };

                _context.Citizens.Add(citizen);
                await _context.SaveChangesAsync();

                return Ok(new { 
                    Message = "Registration successful. Please login to continue.",
                    UserId = user.Id,
                    CitizenId = citizen.UserId
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Registration failed", Error = ex.Message });
            }
        }
    }

    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
