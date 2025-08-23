namespace WebApiFinalProject.DTOs
{
    public class FeatureDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int Price { get; set; }
        public string? Description { get; set; }
    }
}
