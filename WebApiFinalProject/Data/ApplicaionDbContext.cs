        using Microsoft.EntityFrameworkCore;
using WebApiFinalProject.Models;

namespace WebApiFinalProject.Data
{
 
    public class ApplicaionDbContext : DbContext
    {
        public ApplicaionDbContext(DbContextOptions<ApplicaionDbContext> options)
            : base(options)
        {
        }

        public DbSet<Owner> Owners { get; set; } = null!;
        public DbSet<Vehicle> Vehicles { get; set; } = null!;
        public DbSet<Feature> Features { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            // One-to-Many: Owner -> Vehicles
            modelBuilder.Entity<Owner>()
                .HasMany(o => o.Vehicles)
                .WithOne(v => v.Owner)
                .HasForeignKey(v => v.OwnerId);

            // One-to-Many: Vehicle -> Features
            modelBuilder.Entity<Vehicle>()
                .HasMany(v => v.Features)
                .WithOne(f => f.Vehicle)
                .HasForeignKey(f => f.VehicleId);

            // One-to-Many: Role -> Users
            modelBuilder.Entity<Role>()
                .HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId);

            
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, Name = "Admin" },
                new Role { RoleId = 2, Name = "User" }
            );

          
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "Charan",
                    Password = "Charan@123",
                    RoleId = 1 // Admin
                },
                new User
                {
                    Id = 2,
                    Username = "Hadiya",
                    Password = "Hadiya@456",
                    RoleId = 2 // User
                }
            );

            // Seed Owners
            modelBuilder.Entity<Owner>().HasData(
                new Owner { OwnerId = 1, Name = "Yeshwanth",Email="yesh@gmail.com",Age=23, Address = "Andra Pradesh" },
                new Owner { OwnerId = 2, Name = "Chaman", Email = "cham@gmail.com", Age = 27, Address = "Chickballapur" },
                new Owner { OwnerId = 3, Name = "Harish", Email = "hari@gmail.com", Age = 25, Address = "Bangalore" }
            );

            // Seed Vehicles linked to Owners
            modelBuilder.Entity<Vehicle>().HasData(
                new Vehicle { VehicleId = 1, Make = "Toyota", Model = "Camry", date = DateOnly.Parse("2000-08-21"), OwnerId = 1 },
                new Vehicle { VehicleId = 2, Make = "Honda", Model = "CR-V", date = DateOnly.Parse("2002-05-11"), OwnerId = 2 },
                new Vehicle { VehicleId = 3, Make = "Tesla", Model = "Model 3", date = DateOnly.Parse("2021-03-01"), OwnerId = 1 },
                new Vehicle { VehicleId = 4, Make = "Ford", Model = "Mustang", date = DateOnly.Parse("2025-09-27"), OwnerId = 3 }
            );

            // Seed Features linked to Vehicles
            modelBuilder.Entity<Feature>().HasData(
                new Feature { Id = 1, Name = "Sunroof", Price=1000,Description = "Allows natural light in.", VehicleId = 1 },
                new Feature { Id = 2, Name = "Apple CarPlay", Price = 2000, Description = "Connects iPhone to the car's display.", VehicleId = 1 },
                new Feature { Id = 3, Name = "All-Wheel Drive", Price = 3000, Description = "Provides better traction on slick roads.", VehicleId = 2 },
                new Feature { Id = 4, Name = "Autopilot", Price = 2000, Description = "Advanced driver assistance system.", VehicleId = 3 },
                new Feature { Id = 5, Name = "Heated Seats", Price = 4000, Description = "Provides warmth during cold weather.", VehicleId = 4 }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
