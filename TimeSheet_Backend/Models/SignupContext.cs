using Microsoft.EntityFrameworkCore;

namespace TimeSheet_Backend.Models
{
    public class SignupContext : DbContext
    {
        public SignupContext(DbContextOptions<SignupContext> options) : base(options)
        {
            
        }

        public DbSet<Signup> Signups { get; set; }

    
    }
}
