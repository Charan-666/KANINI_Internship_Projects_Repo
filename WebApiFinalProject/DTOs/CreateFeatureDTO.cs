namespace WebApiFinalProject.DTOs
{
    public class CreateFeatureDTO
    {
        public string Name { get; set; } = string.Empty;

        public int Price { get; set; }
        public string? Description { get; set; }
    }
}
