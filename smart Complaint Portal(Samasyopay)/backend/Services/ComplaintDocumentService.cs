using Complaint_2._0.Models;
using Complaint_2._0.Repository;

namespace Complaint_2._0.Services
{
    public class ComplaintDocumentService : IComplaintDocumentService
    {
        private readonly IComplaintDocumentRepository _repo;
        public ComplaintDocumentService(IComplaintDocumentRepository repo)
        {
            _repo = repo;
        }

        public async Task<ComplaintDocument> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }
    }
}
