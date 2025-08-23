namespace WebApiFinalProject.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; }
        public string? RoleName { get; set; } // Role might be null if not included in the query

    }
}
