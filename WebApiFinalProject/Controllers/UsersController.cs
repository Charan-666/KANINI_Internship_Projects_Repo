using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiFinalProject.DTOs;
using WebApiFinalProject.Interfaces;
using WebApiFinalProject.Models;
using WebApiFinalProject.Services;

namespace WebApiFinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            var result = new List<UserDTO>();
            foreach (var u in users)
            {
                result.Add(new UserDTO { Id = u.Id, Username = u.Username, Password = u.Password });
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<UserDTO>> GetById(int id)
        {
            var u = await _userService.GetByIdAsync(id);
            if (u == null) return NotFound();

            return Ok(new UserDTO { Id = u.Id, Username = u.Username, Password = u.Password});
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDTO>> Create(CreateUserDTO dto)
        {
            var u = new User { Username = dto.Username, Password = dto.Password };
            var created = await _userService.AddAsync(u);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                new UserDTO { Id = created.Id, Username = created.Username, Password = created.Password });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDTO>> Update(int id, CreateUserDTO dto)
        {
            var existing = await _userService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Username = dto.Username;

            existing.Password = dto.Password;

            var updated = await _userService.UpdateAsync(existing);
            return Ok(new UserDTO { Id = updated.Id, Username = updated.Username , Password = updated.Password });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _userService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
