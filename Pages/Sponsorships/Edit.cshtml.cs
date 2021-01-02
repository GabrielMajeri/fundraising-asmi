using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Asmi.Fundraising.Data;
using Asmi.Fundraising.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartBreadcrumbs.Attributes;

namespace Asmi.Fundraising.Pages.Sponsorships
{
    [Breadcrumb("ViewData.Title", FromPage = typeof(IndexModel))]
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditModel(AppDbContext context)
        {
            _context = context;
        }

        public ICollection<SelectListItem> Companies { get; set; }
        public ICollection<SelectListItem> Projects { get; set; }
        public ICollection<SelectListItem> Volunteers { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; }
        [BindProperty]
        [Display(Name = "Companie")]
        public int CompanyId { get; set; }
        [BindProperty]
        [Display(Name = "Proiect")]
        public int ProjectId { get; set; }
        [BindProperty]
        [Display(Name = "Data semnării")]
        public DateTime SigningDate { get; set; } = DateTime.Today;
        [BindProperty]
        [Display(Name = "Voluntar")]
        public string VolunteerId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (Id.HasValue)
            {
                var sponsorship = await _context.Sponsorships.FindAsync(Id.Value);
                if (sponsorship == null)
                {
                    return NotFound();
                }

                CompanyId = sponsorship.CompanyId;
                ProjectId = sponsorship.ProjectId;
                SigningDate = sponsorship.SigningDate;
                VolunteerId = sponsorship.VolunteerId;
            }

            var companiesQuery = _context.Companies.Select(
                c => new SelectListItem(c.Name, c.Id.ToString())
            );
            Companies = await companiesQuery.ToListAsync();

            var projectsQuery = _context.Projects.Select(
                p => new SelectListItem(p.FullName, p.Id.ToString())
            );
            Projects = await projectsQuery.ToListAsync();

            var volunteersQuery = _context.Users.Select(
                u => new SelectListItem(u.FullName, u.Id)
            );
            Volunteers = await volunteersQuery.ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return await OnGetAsync();
            }

            var company = await _context.Companies.FindAsync(CompanyId);
            if (company == null)
            {
                return BadRequest();
            }

            var project = await _context.Projects.FindAsync(ProjectId);
            if (project == null)
            {
                return BadRequest();
            }

            AppUser volunteer = null;
            if (!string.IsNullOrEmpty(VolunteerId))
            {
                volunteer = await _context.Users.FindAsync(VolunteerId);
                if (volunteer == null)
                {
                    return BadRequest();
                }
            }

            var sponsorship = new Sponsorship
            {
                Id = Id ?? 0,
                Company = company,
                Project = project,
                SigningDate = SigningDate,
                Volunteer = volunteer,
            };

            _context.Update(sponsorship);
            await _context.SaveChangesAsync();

            return RedirectToPagePermanent("Index");
        }
    }
}
