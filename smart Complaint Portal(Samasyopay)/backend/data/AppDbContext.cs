using Complaint_2._0.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace Complaint_2._0.data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Citizen> Citizens { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<ComplaintType> ComplaintTypes { get; set; }
        public DbSet<ComplaintDocument> ComplaintDocuments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User - Citizen (1:1)
            modelBuilder.Entity<User>()
                .HasOne(u => u.CitizenProfile)
                .WithOne(c => c.User)
                .HasForeignKey<Citizen>(c => c.UserId);

            // User - Agent (1:1)
            modelBuilder.Entity<User>()
                .HasOne(u => u.AgentProfile)
                .WithOne(a => a.User)
                .HasForeignKey<Agent>(a => a.UserId);

            // Citizen - Complaint (1:M)
            modelBuilder.Entity<Citizen>()
                .HasMany(c => c.Complaints)
                .WithOne(comp => comp.Citizen)
                .HasForeignKey(comp => comp.CitizenId)
                .OnDelete(DeleteBehavior.Restrict);

            // Agent - Complaint (1:M)
            modelBuilder.Entity<Agent>()
                .HasMany(a => a.AssignedComplaints)
                .WithOne(c => c.Agent)
                .HasForeignKey(c => c.AgentId)
                .OnDelete(DeleteBehavior.SetNull);

            // ComplaintType - Complaint (1:M)
            modelBuilder.Entity<ComplaintType>()
                .HasMany(ct => ct.Complaints)
                .WithOne(c => c.ComplaintType)
                .HasForeignKey(c => c.ComplaintTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Complaint - ComplaintDocument (1:M)
            modelBuilder.Entity<Complaint>()
                .HasMany(c => c.ComplaintDocuments)
                .WithOne(cd => cd.Complaint)
                .HasForeignKey(cd => cd.ComplaintId)
                .OnDelete(DeleteBehavior.Cascade);

            // Optional: Configure unique constraints
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();







            // Seed Complaint Types
            modelBuilder.Entity<ComplaintType>().HasData(
      new ComplaintType
      {
          Id = 1,
          Name = "Aadhar",
          TypeName = "Aadhar Card Issues"
      },
      new ComplaintType
      {
          Id = 2,
          Name = "VoterID",
          TypeName = "Voter ID Issues"
      },
      new ComplaintType
      {
          Id = 3,
          Name = "BirthCertificate",
          TypeName = "Birth Certificate Issues"
      }
  );


            // Seed Users (passwords: password123, password123, admin123)
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "John Citizen",
                    Email = "john@example.com",
                    PasswordHash = "$2a$11$K5Zy8Zv8Zv8Zv8Zv8Zv8ZuJ8Zv8Zv8Zv8Zv8Zv8Zv8Zv8Zv8Zv8Z",
                    Role = UserRole.Citizen,
                    Photo = null
                },
                new User
                {
                    Id = 2,
                    Name = "Agent Smith",
                    Email = "agent@complaint.com",
                    PasswordHash = "$2a$11$PUnmlFw0gnmDwgcJK6fjse1NFI.k7Eve8FHuKYFa8mb02KyE0/9yG",
                    Role = UserRole.Agent,
                    Photo = null
                },
                new User
                {
                    Id = 3,
                    Name = "Admin User",
                    Email = "admin@complaint.com",
                    PasswordHash = "$2a$11$dAttqu/pCGlCvarGnFKQoO.xNE2wDPSFfuqedGpM28ZRge1ljcZRK",
                    Role = UserRole.Admin,
                    Photo = null
                }
            );

            // Seed Citizen (linked to UserId = 1)
            modelBuilder.Entity<Citizen>().HasData(
                new Citizen
                {
                    UserId = 1,
                    Address = "123 Main Street",
                    PhoneNumber = "9999999999"
                }
            );

            // Seed Agent (linked to UserId = 2)
            modelBuilder.Entity<Agent>().HasData(
                new Agent
                {
                    UserId = 2,
                    Department = "Aadhar"
                }
            );

            // Seed Complaints
            modelBuilder.Entity<Complaint>().HasData(
                new Complaint
                {
                    Id = 1,
                    Title = "Aadhar Card Issue",
                    Description = "Aadhar not updated",
                    ComplaintTypeId = 1,
                    Status = ComplaintStatus.Pending,
                    CitizenId = 1,
                    AgentId = null,
                    CreatedAt = new DateTime(2024, 1, 15, 10, 0, 0),
                    UpdatedAt = new DateTime(2024, 1, 15, 10, 0, 0)
                },
                new Complaint
                {
                    Id = 2,
                    Title = "Voter ID Name Mismatch",
                    Description = "Name mismatch in Voter ID",
                    ComplaintTypeId = 2,
                    Status = ComplaintStatus.Pending,
                    CitizenId = 1,
                    AgentId = null,
                    CreatedAt = new DateTime(2024, 1, 15, 10, 30, 0),
                    UpdatedAt = new DateTime(2024, 1, 15, 10, 30, 0)
                }
            );


            // Seed ComplaintDocuments
            modelBuilder.Entity<ComplaintDocument>().HasData(
     new ComplaintDocument
     {
         Id = 1,
         ComplaintId = 1,
         FileName = "aadhar_doc.pdf",
         ContentType = "application/pdf",
         Data = new byte[] { },
         Type = DocumentType.Uploaded,
         UploadedAt = new DateTime(2024, 1, 15, 10, 0, 0)
     },
     new ComplaintDocument
     {
         Id = 2,
         ComplaintId = 2,
         FileName = "voter_doc.pdf",
         ContentType = "application/pdf",
         Data = new byte[] { },
         Type = DocumentType.Uploaded,
         UploadedAt = new DateTime(2024, 1, 15, 10, 30, 0)
     }
 );

        }

    }
}

