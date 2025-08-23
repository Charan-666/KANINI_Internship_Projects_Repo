namespace WebApiFinalProject.DTOs
{
    public class OwnerDTO
    {
        public int OwnerId { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; }

        public int Age { get; set; }
        public string? Address { get; set; }

    }
}
