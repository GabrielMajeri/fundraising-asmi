using Asmi.Fundraising.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Asmi.Fundraising.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Company> Companies { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// Clears and initializes the database with mock data.
        public void Seed()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
            SaveChanges();

            var companies = new Company[]
            {
                new Company { Name = "Autodesk" },
                new Company { Name = "Mindit" },
                new Company { Name = "Softbinator" },
                new Company { Name = "Fitbit" },
                new Company { Name = "Luxoft" },
                new Company { Name = "Ubisoft" },
                new Company { Name = "Accenture" },
                new Company { Name = "Adobe" },
                new Company { Name = "Microsoft" },
            };
            Companies.AddRange(companies);

            SaveChanges();
        }
    }
}
