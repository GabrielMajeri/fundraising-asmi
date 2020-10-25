using Microsoft.EntityFrameworkCore;

namespace Asmi.Fundraising.Models
{
    public class AppContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }

        public AppContext(DbContextOptions<AppContext> options) : base(options) { }

        /// Clears and initializes the database with mock data.
        public void Seed()
        {
            Database.EnsureCreated();
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
