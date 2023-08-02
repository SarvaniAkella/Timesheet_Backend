using Microsoft.EntityFrameworkCore;

namespace TimeSheet_Backend.Models
{
    public class SignupContext : DbContext
    {
        public SignupContext(DbContextOptions<SignupContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
 
        
        public DbSet<Project> Projects { get; set; }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<TimeSheet> TimeSheets { get; set; }
        public DbSet<role> roles { get; set; }
        



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure any additional model or relationship settings here

            modelBuilder.Entity<Project>().HasData(
                new Project { ProjectId = 1, ProjectName = "Persona Nutrition" },
                new Project { ProjectId = 2, ProjectName = "Puritains" },
                new Project { ProjectId = 3, ProjectName = "Nestle Health Sciences" },
                new Project { ProjectId = 4, ProjectName = "Market Central" },
                new Project { ProjectId = 5, ProjectName = "Family Central" },
                new Project { ProjectId = 6, ProjectName = "Internal POC" },
                new Project { ProjectId = 7, ProjectName = "External POC" },
                new Project { ProjectId = 8, ProjectName = "Marketing & Sales" }
            );


            modelBuilder.Entity<Activity>().HasData(
             new Activity { ActivityId = 1, ActivityName = "Unit Testing" },
             new Activity { ActivityId = 2, ActivityName = "Acceptance Testing" },
             new Activity { ActivityId = 3, ActivityName = "Warranty/MC" },
             new Activity { ActivityId = 4, ActivityName = "System Testing" },
             new Activity { ActivityId = 5, ActivityName = "Coding/Implementation" },
             new Activity { ActivityId = 6, ActivityName = "Design" },
             new Activity { ActivityId = 7, ActivityName = "Support" },
             new Activity { ActivityId = 8, ActivityName = "Integration Testing" },
             new Activity { ActivityId = 9, ActivityName = "Requirements Development" },
             new Activity { ActivityId = 10, ActivityName = "Planning" },
             new Activity { ActivityId = 11, ActivityName = "PTO" }
         );

            modelBuilder.Entity<role>().HasData(
            new role { roleId = 1, roleName = "User" },
            new role { roleId = 2, roleName = "Hr" },
            new role { roleId = 3, roleName = "Admin" }

         );   


            // Add any ot;her seed data or model configurations here
        }

 

    }
}
