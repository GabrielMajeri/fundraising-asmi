using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asmi.Fundraising.Data;
using Asmi.Fundraising.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmartBreadcrumbs.Attributes;

namespace Asmi.Fundraising.Pages.Sponsorships
{
    [Breadcrumb("Sponsorizări", FromPage = typeof(Pages.IndexModel))]
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public List<Sponsorship> Sponsorships { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? CompanyId { get; set; }
        public Company Company { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? ProjectId { get; set; }
        public Project Project { get; set; }

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            IQueryable<Sponsorship> query = _context.Sponsorships
                .AsNoTracking()
                .Include(s => s.Company)
                .Include(s => s.Project)
                .Include(s => s.Volunteer);

            // Filter sponsorships by company
            if (CompanyId.HasValue)
            {
                // Try loading the corresponding company object
                Company = await _context.Companies.FindAsync(CompanyId.Value);
                if (Company == null)
                {
                    return NotFound();
                }

                query = query.Where(s => s.Company.Id == CompanyId);
            }

            // Filter sponsorships by project
            if (ProjectId.HasValue)
            {
                Project = await _context.Projects.FindAsync(ProjectId.Value);
                if (Project == null)
                {
                    return NotFound();
                }

                query = query.Where(s => s.Project.Id == ProjectId);
            }

            query = query.OrderByDescending(s => s.SigningDate);

            Sponsorships = await query.ToListAsync();

            return Page();
        }
    }
}
