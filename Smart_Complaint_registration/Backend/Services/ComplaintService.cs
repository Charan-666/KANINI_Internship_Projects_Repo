using Microsoft.EntityFrameworkCore;
using Smart_Complaint_Registration.Data;
using Smart_Complaint_Registration.Interfaces;
using Smart_Complaint_Registration.Models;

namespace Smart_Complaint_Registration.Services
{
    public class ComplaintService
    {
        private readonly IComplaintRepository _repo;
        private readonly SmartDbContext _context;

        public ComplaintService(IComplaintRepository repo, SmartDbContext context)
        {
            _repo = repo;
            _context = context;
        }

        

        

        public async Task<IEnumerable<Complaint>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<Complaint?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);
        public async Task<Complaint> AddAsync(Complaint entity) => await _repo.AddAsync(entity);
        public async Task<Complaint> UpdateAsync(Complaint entity) => await _repo.UpdateAsync(entity);
        public async Task<bool> DeleteAsync(int id) => await _repo.DeleteAsync(id);




        public async Task<Complaint> SubmitComplaintAsync(Complaint complaint)
        {
            complaint.CreatedDate = DateTime.UtcNow;
            _context.Complaints.Add(complaint);
            await _context.SaveChangesAsync();
            return complaint;
        }



        public async Task<Complaint> UpdateComplaintStatusAsync(int id, int status)
        {
            var complaint = await _context.Complaints.FindAsync(id);
            if (complaint == null) throw new Exception("Complaint not found");

            complaint.Status = status;
            if (status == 2) // Resolved
                complaint.ResolvedDate = DateTime.UtcNow;

            _context.Complaints.Update(complaint);
            await _context.SaveChangesAsync();
            return complaint;
        }

    }
}
