using Asmi.Fundraising.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Asmi.Fundraising.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Project> Projects { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Ensure each project has an unique name and edition combination.
            builder.Entity<Project>()
                .HasIndex(p => new { p.Name, p.Edition })
                .IsUnique();
        }

        /// Clears and initializes the database with mock data.
        public void Seed()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
            SaveChanges();

            var companies = new Company[]
            {
                new Company { Name = "Autodesk", Site = "https://www.autodesk.eu/" },
                new Company { Name = "Mindit", Site = "https://www.mindit.io/" },
                new Company { Name = "Softbinator", Site = "https://www.softbinator.com/" },
                new Company { Name = "Fitbit", Site = "https://www.fitbit.com/global/eu/home" },
                new Company { Name = "Luxoft", Site = "https://www.luxoft.com/" },
                new Company { Name = "Ubisoft", Site = "https://www.ubisoft.com/en-us/" },
                new Company { Name = "Accenture", Site = "https://www.accenture.com/ro-en" },
                new Company { Name = "Adobe", Site = "https://www.adobe.com/ro/" },
                new Company { Name = "Microsoft", Site = "https://www.microsoft.com/ro-ro" },
            };
            Companies.AddRange(companies);

            var projects = new Project[]
            {
                new Project { Name = "Cariere", Edition = "2020" },
                new Project { Name = "SmartHack", Edition = "2020" },
                new Project { Name = "Artă'n Dar", Edition = "2020" },
                new Project { Name = "Cariere", Edition = "2021" }
            };
            Projects.AddRange(projects);

            SaveChanges();
        }
    }
}
