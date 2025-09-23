using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smart_Complaint_Registration.Dto;
using Smart_Complaint_Registration.Interfaces;
using Smart_Complaint_Registration.Models;

namespace Smart_Complaint_Registration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin,Citizen")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
        {
            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = dto.Password, 
                Role = dto.Role,
                CreatedAt = DateTime.UtcNow
            };

            var createdUser = await _userService.CreateUserAsync(user, dto.DepartmentId);
            return Ok(createdUser);
        }
    }
    }
