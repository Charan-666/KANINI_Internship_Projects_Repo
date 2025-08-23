namespace WebApiFinalProject.DTOs
{
    public class CreateVehicleDTO
    {
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public DateOnly date { get; set; }
        public int OwnerId { get; set; }
    }
}
