using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Asmi.Fundraising.Models;

namespace Asmi.Fundraising.Data
{
    public class SeedData
    {
        private readonly AppDbContext _context;

        public SeedData(AppDbContext context)
        {
            _context = context;
        }

        /// Initializes the database with mock data.
        public void Seed()
        {
            var volunteers = _context.Users.ToList();

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
                new Company
                {
                    Name = "Microsoft",
                    Site = "https://www.microsoft.com/ro-ro",
                    Logo = LoadLogo("microsoft.png")
                },
            };

            companies[0].Contacts = new List<Contact>
            {
                new Contact
                {
                    Name = "Ion Popescu",
                    Email = "ion.popescu@example.com"
                },
                new Contact
                {
                    Name = "Ioana Exemplu",
                    Email = "ioana@example.org",
                    Telephone = "0123456780"
                }
            };
            companies[1].Contacts = new List<Contact>
            {
                new Contact
                {
                    Name = "Test Xulescu",
                    Telephone = "0700111222"
                }
            };

            _context.Companies.AddRange(companies);

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
            _context.Projects.AddRange(projects);

            var sponsorships = new Sponsorship[]
            {
                new Sponsorship
                {
                    Company = companies[0],
                    Project = projects[1],
                    SigningDate = DateTime.Today
                },
                new Sponsorship
                {
                    Company = companies[2],
                    Project = projects[0],
                    SigningDate = DateTime.Today.AddDays(-7),
                    Volunteer = volunteers[0]
                }
            };
            _context.Sponsorships.AddRange(sponsorships);

            _context.SaveChanges();
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
