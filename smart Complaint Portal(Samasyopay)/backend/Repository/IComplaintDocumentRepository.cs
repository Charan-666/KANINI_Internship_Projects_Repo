using Complaint_2._0.Models;

namespace Complaint_2._0.Repository
{
    public interface IComplaintDocumentRepository
    {
        Task<ComplaintDocument> GetByIdAsync(int id);
    }
}
