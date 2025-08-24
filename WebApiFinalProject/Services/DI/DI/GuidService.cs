namespace WebApiFinalProject.Services.DI
{
    public class GuidService : IGuidService
    {
        private readonly string _guid;

        public GuidService()
        {
            _guid = Guid.NewGuid().ToString();
        }

        public string GetGuid()
        {
            return _guid;
        }
    }
}
