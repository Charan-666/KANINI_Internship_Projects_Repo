using Complaint_2._0.dto;
using Complaint_2._0.Models;
using Complaint_2._0.Repository;

namespace Complaint_2._0.Services
{
  
        public class CitizenService : ICitizenService
        {
            private readonly ICitizenRepository _repo;

            public CitizenService(ICitizenRepository repo)
            {
                _repo = repo;
            }

            public Task<Citizen> GetProfileAsync(int citizenId) =>
                _repo.GetProfileAsync(citizenId);

            public async Task<Citizen> UpdateProfileAsync(int citizenId, UpdateCitizenDTO dto)
            {
                var citizen = await _repo.GetProfileAsync(citizenId)
                              ?? throw new Exception("Citizen not found");

                citizen.Address = dto.Address;
                citizen.PhoneNumber = dto.PhoneNumber;

                if (dto.Photo != null)
                {
                    using var ms = new MemoryStream();
                    await dto.Photo.CopyToAsync(ms);
                    citizen.User.Photo = ms.ToArray();
                }

                return await _repo.UpdateProfileAsync(citizen);
            }

            public async Task<Complaint> RaiseComplaintAsync(int citizenId, CreateComplaintDTO dto)
            {
                var complaint = new Complaint
                {
                    Title = dto.Title,
                    Description = dto.Description,
                    ComplaintTypeId = dto.ComplaintTypeId,
                    Status = ComplaintStatus.Pending,
                    CitizenId = citizenId,
                    AgentId = null,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    ComplaintDocuments = new List<ComplaintDocument>()
                };

                if (dto.Documents != null)
                {
                    foreach (var doc in dto.Documents)
                    {
                        using var ms = new MemoryStream();
                        await doc.CopyToAsync(ms);
                        complaint.ComplaintDocuments.Add(new ComplaintDocument
                        {
                            FileName = doc.FileName,
                            Data = ms.ToArray(),
                            ContentType = doc.ContentType,
                            Type = DocumentType.Uploaded,
                            UploadedAt = DateTime.UtcNow
                        });
                    }
                }

                return await _repo.CreateComplaintAsync(complaint);
            }

            public Task<IEnumerable<Complaint>> GetMyComplaintsAsync(int citizenId) =>
                _repo.GetMyComplaintsAsync(citizenId);

            public Task<Complaint> GetComplaintDetailsAsync(int citizenId, int complaintId) =>
                _repo.GetComplaintDetailsAsync(citizenId, complaintId);

            public async Task<Complaint> UpdateComplaintAsync(int citizenId, UpdateComplaintDTO dto)
            {
                var complaint = await _repo.GetComplaintDetailsAsync(citizenId, dto.ComplaintId)
                               ?? throw new Exception("Complaint not found");

                if (complaint.AgentId != null)
                    throw new Exception("Cannot update complaint after it has been assigned");

                complaint.Title = dto.Title;
                complaint.Description = dto.Description;
                complaint.ComplaintTypeId = dto.ComplaintTypeId;
                complaint.UpdatedAt = DateTime.UtcNow;

                return await _repo.UpdateComplaintAsync(complaint);
            }

            public Task<bool> DeleteComplaintAsync(int citizenId, int complaintId) =>
                _repo.DeleteComplaintAsync(citizenId, complaintId);
        }
    }
