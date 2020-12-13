using System;
using System.Collections.Generic;
using System.IO;
using Asmi.Fundraising.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Asmi.Fundraising.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Image> Images { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Contact> Contacts { get; set; }

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
                new Company
                {
                    Name = "Accenture",
                    Site = "https://www.accenture.com/ro-en",
                    Logo = LoadLogo("accenture.png")
                },
                new Company { Name = "Adobe", Site = "https://www.adobe.com/ro/" },
                new Company { Name = "Microsoft", Site = "https://www.microsoft.com/ro-ro" },
            };

            companies[0].Contacts = new List<Contact>
            {
                new Contact
                {
                    Name = "Ion Popescu",
                },
            };
            companies[1].Contacts = new List<Contact>
            {
                new Contact
                {
                    Name = "Test Xulescu",
                }
            };

            Companies.AddRange(companies);

            var projects = new Project[]
            {
                new Project { Name = "Cariere", Edition = "2020" },
                new Project { Name = "SmartHack", Edition = "2020" },
                new Project
                {
                    Name = "Artă'n Dar",
                    Edition = "2020",
                    Logo = LoadLogo("artandar.png")
                },
                new Project { Name = "Cariere", Edition = "2021" }
            };
            Projects.AddRange(projects);

            SaveChanges();
        }

        private static Image LoadLogo(string name)
        {
            return LoadImage($"SeedData/Logos/{name}");
        }

        private static Image LoadImage(string path)
        {
            var dotIndex = path.LastIndexOf('.');
            if (dotIndex < 0)
            {
                throw new Exception("Image path is missing file extension");
            }

            var extension = path.Substring(dotIndex + 1);
            var contentType = Image.ContentTypeFromFileExtension(extension);
            if (contentType == null)
            {
                throw new Exception("Unknown image type");
            }

            return new Image
            {
                Data = File.ReadAllBytes(path),
                ContentType = contentType
            };
        }
    }
}
