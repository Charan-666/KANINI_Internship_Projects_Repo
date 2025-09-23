using Smart_Complaint_Registration.Data;
using Smart_Complaint_Registration.Models;

namespace Smart_Complaint_Registration.Services
{
    public class ComplaintAssignmentService
    {
        private readonly SmartDbContext _context;

        public ComplaintAssignmentService(SmartDbContext context)
        {
            _context = context;
        }

        public async Task AssignComplaintToDepartment(int complaintId, int departmentId)
        {
            var collaboration = new ComplaintDepartmentCollaboration
            {
                ComplaintId = complaintId,
                DepartmentId = departmentId,
                CollaborationStatus = "Pending"
            };
            _context.ComplaintDepartmentCollaborations.Add(collaboration);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCollaborationStatus(int collaborationId, string status)
        {
            var collab = await _context.ComplaintDepartmentCollaborations.FindAsync(collaborationId);
            if (collab == null) throw new Exception("Collaboration not found");

            collab.CollaborationStatus = status;
            _context.ComplaintDepartmentCollaborations.Update(collab);
            await _context.SaveChangesAsync();
        }
    }
}
