namespace WebApiFinalProject.DTOs
{
    public class VehicleDTO
    {
      
        public int VehicleId { get; set; }
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public DateOnly date { get; set; }
        public int OwnerId { get; set; }
    }
}
