using Microsoft.EntityFrameworkCore;

namespace TimeSheet_Backend.Models
{
    public class SignupContext : DbContext
    {
        public SignupContext(DbContextOptions<SignupContext> options) : base(options)
        {
            
        }

        public DbSet<UserSignup> Users { get; set; }
        public DbSet<AdminSignup> Admins { get; set; }
        public DbSet<HrSignup> HRs { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }

    }
}
