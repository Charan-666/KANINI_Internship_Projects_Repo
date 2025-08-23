using System.ComponentModel.DataAnnotations;

namespace WebApiFinalProject.DTOs
{
    public class CreateUserDTO
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public int RoleId { get; set; }
    }
}
