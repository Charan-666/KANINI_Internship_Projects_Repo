using Complaint_2._0.Models;

namespace Complaint_2._0.Services
{
    public interface IComplaintDocumentService
    {
        Task<ComplaintDocument> GetByIdAsync(int id);
    }
}
