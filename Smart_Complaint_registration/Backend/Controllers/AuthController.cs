using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smart_Complaint_Registration.Dto;
using Smart_Complaint_Registration.Interfaces;
using Smart_Complaint_Registration.Services;

namespace Smart_Complaint_Registration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "Citizen")]
        public async Task<ActionResult<CitizenResponseDto>> RegisterCitizen([FromForm] RegisterCitizenDto dto)
        {
            var result = await _userService.RegisterCitizenAsync(dto);
            return Ok(result);
        }


        [HttpPost("login")]
        [Authorize(Roles = "Head,Agent,Admin,Citizen")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _userService.LoginAsync(dto);
            if (user == null) return Unauthorized("Invalid credentials");

            return Ok(new { message = "Login successful", user });
        }
    }
}
