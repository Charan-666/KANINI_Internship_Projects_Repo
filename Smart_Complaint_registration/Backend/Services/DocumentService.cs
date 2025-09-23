using Microsoft.EntityFrameworkCore;
using Smart_Complaint_Registration.Data;
using Smart_Complaint_Registration.Models;
using System;
using static Smart_Complaint_Registration.Models.User;

namespace Smart_Complaint_Registration.Services
{
    public class DocumentService
    {
        private readonly SmartDbContext _context;

        public DocumentService(SmartDbContext context)
        {
            _context = context;
        }

        // Save uploaded document
        public async Task<DocumentEvidence> AddDocumentAsync(DocumentEvidence document, int complaintId)
        {
            _context.DocumentEvidences.Add(document);
            await _context.SaveChangesAsync();

            var complaintDoc = new ComplaintDocumentEvidence
            {
                ComplaintId = complaintId,
                DocumentEvidenceId = document.DocumentEvidenceId
            };

            _context.ComplaintDocumentEvidences.Add(complaintDoc);
            await _context.SaveChangesAsync();

            return document;
        }

        // Get all documents linked to complaint
        public async Task<IEnumerable<DocumentEvidence>> GetDocumentsByComplaintAsync(int complaintId)
        {
            return await _context.ComplaintDocumentEvidences
                .Include(cd => cd.DocumentEvidence)
                .ThenInclude(de => de.UploadedBy)
                .Where(cd => cd.ComplaintId == complaintId)
                .Select(cd => cd.DocumentEvidence)
                .ToListAsync();
        }

        // Get single document
        public async Task<DocumentEvidence?> GetDocumentByIdAsync(int documentId)
        {
            return await _context.DocumentEvidences
                .Include(d => d.UploadedBy)
                .FirstOrDefaultAsync(d => d.DocumentEvidenceId == documentId);
        }
    }
}