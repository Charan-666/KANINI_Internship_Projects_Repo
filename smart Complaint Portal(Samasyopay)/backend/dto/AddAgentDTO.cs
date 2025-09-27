namespace Complaint_2._0.dto
{
    public class AddAgentDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        // Other User fields if needed

        // Agent-specific fields
        public string Department { get; set; }
        
    }
}
