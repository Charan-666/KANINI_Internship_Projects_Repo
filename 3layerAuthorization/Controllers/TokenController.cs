using Authorizationwebapis.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authorizationwebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly LockerDbContext _con;
        private readonly IConfiguration _configuration;

        public TokenController(LockerDbContext con, IConfiguration configuration)
        {
            _con = con;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Post(User userData)
        {
            if (userData != null && !string.IsNullOrEmpty(userData.Email) &&
                !string.IsNullOrEmpty(userData.Password))
            {
                var user = await GetUser(userData.Email, userData.Password);

                if (user != null && !string.IsNullOrEmpty(user.Email))
                {
                    var token = GenerateToken(user);
                    return Ok(new { token });
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest("Invalid request data");
            }
        }

        private async Task<User> GetUser(string email, string password)
        {
            return await _con.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password)
                ?? new User();
        }

        private string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenKey"]!));

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Name ?? string.Empty),
                new Claim(ClaimTypes.Role,user.role)
            };

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(2),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}

