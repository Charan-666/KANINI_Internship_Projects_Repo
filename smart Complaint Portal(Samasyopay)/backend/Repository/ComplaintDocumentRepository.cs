using Complaint_2._0.data;
using Complaint_2._0.Models;
using Microsoft.EntityFrameworkCore;

namespace Complaint_2._0.Repository
{
    public class ComplaintDocumentRepository : IComplaintDocumentRepository
    {
        private readonly AppDbContext _context;
        public ComplaintDocumentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ComplaintDocument> GetByIdAsync(int id)
        {
            return await _context.ComplaintDocuments.FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}
