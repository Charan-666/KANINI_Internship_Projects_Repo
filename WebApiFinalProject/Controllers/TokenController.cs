using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.Text;
using WebApiFinalProject.DTOs;
using WebApiFinalProject.Interfaces;
using WebApiFinalProject.Services;

namespace WebApiFinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly SymmetricSecurityKey _key;
        private readonly ITokenService _tokenService;
        private readonly IUser _userService;

        public TokenController(IConfiguration configuration, IUser userService, ITokenService tokenService)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
            _tokenService = tokenService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            if (loginDto == null || string.IsNullOrEmpty(loginDto.Username) || string.IsNullOrEmpty(loginDto.Password))
                return BadRequest("Username and password are required");

            var user = await _userService.GetByUsernameAsync(loginDto.Username);

            if (user == null)
                return Unauthorized("Invalid username");


            if (user.Password != loginDto.Password)
                return Unauthorized("Invalid password");

            var token = _tokenService.GenerateToken(user);

            return Ok(new
            {
                token,
                username = user.Username,
                role = user.Role!
            });
        }
    }
}

