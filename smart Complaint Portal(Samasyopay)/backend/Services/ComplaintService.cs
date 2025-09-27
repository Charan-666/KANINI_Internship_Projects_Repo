using Complaint_2._0.dto;
using Complaint_2._0.Models;
using Complaint_2._0.Repository;

namespace Complaint_2._0.Services
{
    public class ComplaintService : IComplaintService
    {
        private readonly IComplaintRepository _repo;

        public ComplaintService(IComplaintRepository repo)
        {
            _repo = repo;
        }

        public async Task<Complaint> CreateComplaintAsync(int citizenId, CreateComplaintDTO dto)
        {
            var complaint = new Complaint
            {
                Title = dto.Title,
                Description = dto.Description,
                ComplaintTypeId = dto.ComplaintTypeId,
                Status = ComplaintStatus.Pending,
                CitizenId = citizenId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            if (dto.Documents != null && dto.Documents.Count > 0)
            {
                complaint.ComplaintDocuments = new List<ComplaintDocument>();

                foreach (var doc in dto.Documents)
                {
                    using var ms = new MemoryStream();
                    await doc.CopyToAsync(ms);

                    complaint.ComplaintDocuments.Add(new ComplaintDocument
                    {
                        FileName = doc.FileName,
                        ContentType = string.IsNullOrWhiteSpace(doc.ContentType) ? "application/octet-stream" : doc.ContentType,
                        Data = ms.ToArray(),
                        Type = DocumentType.Uploaded,
                        UploadedAt = DateTime.UtcNow
                    });
                }
            }

            return await _repo.AddAsync(complaint);
        }


        public async Task<List<Complaint>> GetAllComplaintsAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Complaint> GetComplaintByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<List<Complaint>> GetComplaintsByCitizenAsync(int citizenId)
        {
            return await _repo.GetByCitizenIdAsync(citizenId);
        }

        public async Task<List<Complaint>> GetComplaintsByAgentAsync(int agentId)
        {
            return await _repo.GetByAgentIdAsync(agentId);
        }

        public async Task<Complaint> UpdateComplaintStatusAsync(UpdateComplaintStatusDTO dto)
        {
            var complaint = await _repo.GetByIdAsync(dto.ComplaintId);
            if (complaint == null) throw new Exception("Complaint not found");

            complaint.Status = dto.Status;
            complaint.UpdatedAt = DateTime.UtcNow;

            if (dto.SolutionDocument != null && dto.SolutionDocument.Length > 0)
            {
                complaint.ComplaintDocuments ??= new List<ComplaintDocument>();

                using var ms = new MemoryStream();
                await dto.SolutionDocument.CopyToAsync(ms);

                complaint.ComplaintDocuments.Add(new ComplaintDocument
                {
                    FileName = dto.SolutionDocument.FileName,
                    ContentType = string.IsNullOrWhiteSpace(dto.SolutionDocument.ContentType) ? "application/octet-stream" : dto.SolutionDocument.ContentType,
                    Data = ms.ToArray(),
                    Type = DocumentType.Solved,
                    UploadedAt = DateTime.UtcNow
                });
            }

            return await _repo.UpdateAsync(complaint);
        }


        public async Task<Complaint> AssignComplaintAsync(AssignComplaintDTO dto)
        {
            var complaint = await _repo.GetByIdAsync(dto.ComplaintId);
            if (complaint == null) throw new Exception("Complaint not found");

            complaint.AgentId = dto.AgentId;
            complaint.Status = ComplaintStatus.Assigned;
            complaint.UpdatedAt = DateTime.UtcNow;

            return await _repo.UpdateAsync(complaint);
        }

        public async Task DeleteComplaintAsync(int complaintId)
        {
            var complaint = await _repo.GetByIdAsync(complaintId);
            if (complaint == null) throw new Exception("Complaint not found");

            await _repo.DeleteAsync(complaint);
        }
    }
}
