using Complaint_2._0.dto;
using Complaint_2._0.Models;
using Complaint_2._0.Repository;

namespace Complaint_2._0.Services
{
  
        public class AgentService : IAgentService
        {
            private readonly IAgentRepository _repo;

            public AgentService(IAgentRepository repo)
            {
                _repo = repo;
            }

            public Task<IEnumerable<Complaint>> GetAssignedComplaintsAsync(int agentId) =>
                _repo.GetAssignedComplaintsAsync(agentId);

            public Task<Complaint> GetComplaintDetailsAsync(int agentId, int complaintId) =>
                _repo.GetComplaintDetailsAsync(agentId, complaintId);

            public async Task<Complaint> UpdateComplaintStatusAsync(int agentId, int complaintId, ComplaintStatus status)
            {
                var complaint = await _repo.GetComplaintDetailsAsync(agentId, complaintId)
                               ?? throw new Exception("Complaint not found");

                complaint.Status = status;
                complaint.UpdatedAt = DateTime.UtcNow;

                return await _repo.UpdateComplaintStatusAsync(complaint);
            }

            public async Task<Complaint> UploadSolutionDocumentAsync(int agentId, int complaintId, UploadDto solutionFile)
            {
                if (solutionFile == null || solutionFile.File.Length == 0)
                    throw new Exception("Solution file is required");

                using var ms = new MemoryStream();
                await solutionFile.File.CopyToAsync(ms);

                var document = new ComplaintDocument
                {
                    FileName = solutionFile.File.FileName,
                    ContentType = solutionFile.File.ContentType,
                    Data = ms.ToArray(),
                    Type = DocumentType.Solved,
                    UploadedAt = DateTime.UtcNow
                };

                return await _repo.UploadSolutionDocumentAsync(complaintId, document);
            }

            public Task<IEnumerable<Complaint>> SearchComplaintsAsync(int agentId, int? typeId, DateTime? from, DateTime? to, string citizenName) =>
                _repo.SearchComplaintsAsync(agentId, typeId, from, to, citizenName);
        }
    }

