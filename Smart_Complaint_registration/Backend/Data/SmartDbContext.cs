using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Smart_Complaint_Registration.Models;
using static Smart_Complaint_Registration.Models.User;

namespace Smart_Complaint_Registration.Data
{
    public class SmartDbContext : DbContext
    {
        public SmartDbContext(DbContextOptions<SmartDbContext> options) : base(options) { }

        public DbSet<Citizen> Citizens { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentUser> DepartmentUsers { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<ComplaintCategory> ComplaintCategories { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Severity> Severities { get; set; }
        public DbSet<DocumentEvidence> DocumentEvidences { get; set; }
        public DbSet<ComplaintDepartmentCollaboration> ComplaintDepartmentCollaborations { get; set; }
        public DbSet<ComplaintDocumentEvidence> ComplaintDocumentEvidences { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Citizen)
                .WithOne(c => c.User)
                .HasForeignKey<User>(u => u.CitizenId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.DepartmentUser)
                .WithOne(d => d.User)
                .HasForeignKey<User>(u => u.DepartmentUserId);

            // Enum conversion for UserRole
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>();



            // Citizen - Complaint (one-to-many)
            modelBuilder.Entity<Citizen>()
                .HasMany(c => c.Complaints)
                .WithOne(cmp => cmp.Citizen)
                .HasForeignKey(cmp => cmp.CitizenId)
                .OnDelete(DeleteBehavior.Cascade);

            // Complaint - Citizen relationship
            modelBuilder.Entity<Complaint>()
                .HasOne(c => c.Citizen)
                .WithMany(cit => cit.Complaints)
                .HasForeignKey(c => c.CitizenId);

            // Complaint - Priority (many-to-one)
            modelBuilder.Entity<Complaint>()
                .HasOne(cmp => cmp.Priority)
                .WithMany(p => p.Complaints)
                .HasForeignKey(cmp => cmp.PriorityId)
                .OnDelete(DeleteBehavior.Restrict);

            // Complaint - Severity (many-to-one)
            modelBuilder.Entity<Complaint>()
                .HasOne(cmp => cmp.Severity)
                .WithMany(s => s.Complaints)
                .HasForeignKey(cmp => cmp.SeverityId)
                .OnDelete(DeleteBehavior.Restrict);

            // Complaint - ComplaintCategory (many-to-one)
            modelBuilder.Entity<Complaint>()
                .HasOne(cmp => cmp.ComplaintCategory)
                .WithMany(cat => cat.Complaints)
                .HasForeignKey(cmp => cmp.ComplaintCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Department - DepartmentUser (one-to-many)
            modelBuilder.Entity<DepartmentUser>()
                .HasOne(d => d.Department)
                .WithMany(dep => dep.DepartmentUsers)
                .HasForeignKey(d => d.DepartmentId);

            // Explicit Many-to-Many: Complaint - DocumentEvidence via ComplaintDocumentEvidence
            modelBuilder.Entity<ComplaintDocumentEvidence>()
                .HasKey(cde => new { cde.ComplaintId, cde.DocumentEvidenceId });

            modelBuilder.Entity<ComplaintDocumentEvidence>()
                .HasOne(cde => cde.Complaint)
                .WithMany(c => c.ComplaintDocuments)
                .HasForeignKey(cde => cde.ComplaintId);

            modelBuilder.Entity<ComplaintDocumentEvidence>()
                .HasOne(cde => cde.DocumentEvidence)
                .WithMany(d => d.ComplaintDocuments)
                .HasForeignKey(cde => cde.DocumentEvidenceId);

            // Explicit Many-to-Many: Complaint - Department via ComplaintDepartmentCollaboration
            modelBuilder.Entity<ComplaintDepartmentCollaboration>()
                .HasKey(cd => cd.Id);

            modelBuilder.Entity<ComplaintDepartmentCollaboration>()
                .HasOne(cd => cd.Complaint)
                .WithMany(c => c.ComplaintDepartments)
                .HasForeignKey(cd => cd.ComplaintId);

            modelBuilder.Entity<ComplaintDepartmentCollaboration>()
                .HasOne(cd => cd.Department)
                .WithMany(d => d.ComplaintDepartments)
                .HasForeignKey(cd => cd.DepartmentId);

            // Complaint - StatusHistory (one-to-many)
            //modelBuilder.Entity<StatusHistory>()
            //    .HasOne(sh => sh.Complaint)
            //    .WithMany(c => c.StatusHistories)
            //    .HasForeignKey(sh => sh.ComplaintId);

            //// Complaint - Notification (one-to-many)
            //modelBuilder.Entity<Notification>()
            //    .HasOne(n => n.Complaint)
            //    .WithMany(c => c.Notifications)
            //    .HasForeignKey(n => n.ComplaintId)
            //    .OnDelete(DeleteBehavior.SetNull);

            //// Complaint - ComplaintSLA (one-to-one)
            //modelBuilder.Entity<Complaint>()
            //    .HasOne(c => c.ComplaintSla)
            //    .WithOne(sla => sla.Complaint)
            //    .HasForeignKey<ComplaintSLA>(sla => sla.ComplaintId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //// ComplaintCategory - ComplaintSLA (one-to-many)
            //modelBuilder.Entity<ComplaintSLA>()
            //    .HasOne(sla => sla.ComplaintCategory)
            //    .WithMany(cc => cc.ComplaintSlas)
            //    .HasForeignKey(sla => sla.ComplaintCategoryId)
            //    .OnDelete(DeleteBehavior.Restrict);



            // Priorities
            modelBuilder.Entity<Priority>().HasData(
                new Priority { PriorityId = 1, Name = "Low" },
                new Priority { PriorityId = 2, Name = "Medium" },
                new Priority { PriorityId = 3, Name = "High" },
                new Priority { PriorityId = 4, Name = "Critical" }
            );

            // Severities
            modelBuilder.Entity<Severity>().HasData(
                new Severity { SeverityId = 1, Name = "Minor" },
                new Severity { SeverityId = 2, Name = "Major" },
                new Severity { SeverityId = 3, Name = "Critical" }
            );

            // Complaint Categories
            modelBuilder.Entity<ComplaintCategory>().HasData(
                new ComplaintCategory { ComplaintCategoryId = 1, CategoryName = "Aadhaar Card Issue" , Description = "Complaints related to Aadhaar card enrollment, updates, and corrections." },
                new ComplaintCategory { ComplaintCategoryId = 2, CategoryName = "Voter ID Requests", Description = "Complaints and requests regarding voter ID issuance, corrections, and updates." },
                new ComplaintCategory { ComplaintCategoryId = 3, CategoryName = "Birth Certificate Complaints", Description = "Issues and complaints related to birth certificate registration and corrections." }
            );

            // Departments
            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = 1, Name = "Aadhaar Department", ContactInfo = "aadhaar@gov.in" },
                new Department { DepartmentId = 2, Name = "Voter ID Department", ContactInfo = "voterid@gov.in" },
                new Department { DepartmentId = 3, Name = "Birth Certificate Department", ContactInfo = "birthcert@gov.in" }
            );

            // Citizens
            modelBuilder.Entity<Citizen>().HasData(
                new Citizen { CitizenId = 1, Name = "Ravi Kumar", Email = "ravi@example.com", Phone = "9923456789", Address = "Bangalore", AadhaarNumber = "1234-5678-9012", ProfilePhoto = null },
                new Citizen { CitizenId = 2, Name = "Anita Singh", Email = "anita@example.com", Phone = "8845678875", Address = "Mangalore", AadhaarNumber = "2345-6789-0123" , ProfilePhoto = null }
            );

            // Department Users
            modelBuilder.Entity<DepartmentUser>().HasData(
                new DepartmentUser { DepartmentUserId = 1, Name = "Ajay Sharma", Email = "ajay@aadhaar.gov", Phone = "7787452315", Role = "Head", DepartmentId = 1 },
                new DepartmentUser { DepartmentUserId = 2, Name = "Sunita Verma", Email = "sunita@voterid.gov", Phone = "6676893463", Role = "Agent", DepartmentId = 2 }
            );

            // Complaints
            modelBuilder.Entity<Complaint>().HasData(
                new Complaint
                {
                    ComplaintId = 1,
                    Title = "Aadhaar card not received",
                    Description = "I submitted the Aadhaar update request last month but have not received the card.",
                    Status = 0, // Pending
                    CitizenId = 1,
                    PriorityId = 3,
                    SeverityId = 2,
                    ComplaintCategoryId = 1,
                    CreatedDate = new DateTime(2025, 9, 13, 0, 0, 0, DateTimeKind.Utc),
                    DueDate = new DateTime(2025, 9, 23, 0, 0, 0, DateTimeKind.Utc)
                },
                new Complaint
                {
                    ComplaintId = 2,
                    Title = "Voter ID correction needed",
                    Description = "My date of birth is incorrect on my voter ID card.",
                    Status = 0,
                    CitizenId = 2,
                    PriorityId = 2,
                    SeverityId = 1,
                    ComplaintCategoryId = 2,
                    CreatedDate = new DateTime(2025, 9, 13, 0, 0, 0, DateTimeKind.Utc),
                    DueDate = new DateTime(2025, 9, 23, 0, 0, 0, DateTimeKind.Utc) // Overdue
                }
            );

            // ComplaintDepartmentCollaborations
            modelBuilder.Entity<ComplaintDepartmentCollaboration>().HasData(
                new ComplaintDepartmentCollaboration { Id = 1, ComplaintId = 1, DepartmentId = 1, CollaborationStatus = "In Progress" },
                new ComplaintDepartmentCollaboration { Id = 2, ComplaintId = 2, DepartmentId = 2, CollaborationStatus = "Pending" }
            );

            // Document Evidences
            //modelBuilder.Entity<DocumentEvidence>().HasData(
            //    new DocumentEvidence { DocumentEvidenceId = 1, FileName = "charan.jpg", FilePath = "/uploads/documents/charan.jpg", FileType = "image/jpeg", UploadedAt = new DateTime(2025, 9, 13, 0, 0, 0, DateTimeKind.Utc)},
            //    new DocumentEvidence { DocumentEvidenceId = 2, FileName = "profilec.pdf", FilePath = "/uploads/documents/profilec.pdf", FileType = "application/pdf", UploadedAt = new DateTime(2025, 9, 13, 0, 0, 0, DateTimeKind.Utc) }
            //);

            // ComplaintDocumentEvidences (Join table)
            //modelBuilder.Entity<ComplaintDocumentEvidence>().HasData(
            //    new ComplaintDocumentEvidence { ComplaintId = 1, DocumentEvidenceId = 1 },
            //    new ComplaintDocumentEvidence { ComplaintId = 2, DocumentEvidenceId = 2 }
            //);

            //// Status Histories
            //modelBuilder.Entity<StatusHistory>().HasData(
            //    new StatusHistory { StatusHistoryId = 1, ComplaintId = 1, PreviousStatus = "None", NewStatus = "Pending", ChangedByUserId = 1, ChangedDate = DateTime.UtcNow.AddDays(-7), Note = "Complaint Registered" },
            //    new StatusHistory { StatusHistoryId = 2, ComplaintId = 2, PreviousStatus = "None", NewStatus = "Pending", ChangedByUserId = 2, ChangedDate = DateTime.UtcNow.AddDays(-10), Note = "Complaint Registered" }
            //);

            //// Notifications
            //modelBuilder.Entity<Notification>().HasData(
            //    new Notification { NotificationId = 1, ComplaintId = 1, RecipientUserId = 1, Message = "Your complaint has been registered successfully.", IsRead = false, CreatedDate = DateTime.UtcNow.AddDays(-7) },
            //    new Notification { NotificationId = 2, ComplaintId = 2, RecipientUserId = 2, Message = "Your complaint is overdue, please follow up.", IsRead = false, CreatedDate = DateTime.UtcNow.AddDays(-1) }
            //);

            //// Complaint SLA
            //modelBuilder.Entity<ComplaintSLA>().HasData(
            //    new ComplaintSLA { ComplaintSLAId = 1, SLA_Days = 10, ComplaintCategoryId = 1, ComplaintId = 1, ResolutionDue = DateTime.UtcNow.AddDays(3) },
            //    new ComplaintSLA { ComplaintSLAId = 2, SLA_Days = 7, ComplaintCategoryId = 2, ComplaintId = 2, ResolutionDue = DateTime.UtcNow.AddDays(-1) }
            //);

            //// Audit Trails - Simple examples
            //modelBuilder.Entity<AuditTrail>().HasData(
            //    new AuditTrail { AuditTrailId = 1, EntityType = "Complaint", EntityId = 1, ActionType = "Created", PerformedByUserId = 1, PerformedOn = DateTime.UtcNow.AddDays(-7) },
            //    new AuditTrail { AuditTrailId = 2, EntityType = "Complaint", EntityId = 2, ActionType = "Created", PerformedByUserId = 2, PerformedOn = DateTime.UtcNow.AddDays(-10) }
            //);


            

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Username = "Admin",
                    Email = "admin@system.com",
                    PasswordHash = "AdminPassword123 ",
                    Role = UserRole.Admin,
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 9, 13, 0, 0, 0, DateTimeKind.Utc)
                    // No CitizenId or DepartmentUserId - standalone admin user
                },
                new User
                {
                    UserId = 2,
                    Username = "Ajay Sharma",
                    Email = "ajay@aadhaar.gov",
                    PasswordHash = "AdminPassword123",
                    Role = UserRole.Head,
                    DepartmentUserId = 1, // Matches DepartmentUser seeded with ID=1
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 9, 13, 0, 0, 0, DateTimeKind.Utc)
                },
                new User
                {
                    UserId = 3,
                    Username = "Sunita Verma",
                    Email = "sunita@voterid.gov",
                    PasswordHash = "AdminPassword123 ",
                    Role = UserRole.Agent,
                    DepartmentUserId = 2, // Matches DepartmentUser seeded with ID=2
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 9, 13, 0, 0, 0, DateTimeKind.Utc)
                }
            );

        }
    }
}
