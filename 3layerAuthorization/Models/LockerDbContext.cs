using Microsoft.EntityFrameworkCore;

namespace Authorizationwebapis.Models
{
    public class LockerDbContext :DbContext
    {
        public LockerDbContext(DbContextOptions<LockerDbContext> options)
           : base(options)
        {
        }

       public DbSet<User> Users { get; set; }
        public DbSet<Locker> Lockers { get; set; }


       
        }
    }
