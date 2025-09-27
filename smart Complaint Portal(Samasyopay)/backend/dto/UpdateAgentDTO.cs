namespace Complaint_2._0.dto
{
    public class UpdateAgentDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Agent-specific fields
        public string Department { get; set; }
    }
}
