using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smart_Complaint_Registration.Data;
using Smart_Complaint_Registration.Dto;
using Smart_Complaint_Registration.Interfaces;

namespace Smart_Complaint_Registration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   public class TokenController : ControllerBase
{
    private readonly SmartDbContext _context;
    private readonly ITokenService _tokenService;

    public TokenController(SmartDbContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == dto.UsernameOrEmail && u.PasswordHash == dto.Password);

        if (user == null)
            return Unauthorized("Invalid username or password.");

        var token = _tokenService.CreateToken(user);

        return Ok(new { token, userId = user.UserId, role = user.Role.ToString() });
    }
}
}
