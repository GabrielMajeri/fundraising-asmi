using Asmi.Fundraising.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Asmi.Fundraising.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public DbSet<Image> Images { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Sponsorship> Sponsorships { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Ensure each project has an unique name and edition combination.
            builder.Entity<Project>()
                .HasIndex(p => new { p.Name, p.Edition })
                .IsUnique();
        }
    }
}
