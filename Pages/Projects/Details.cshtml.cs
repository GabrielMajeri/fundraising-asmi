using System.Threading.Tasks;
using Asmi.Fundraising.Data;
using Asmi.Fundraising.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Asmi.Fundraising.Pages.Projects
{
    [Breadcrumb("ViewData.Title", FromPage = typeof(IndexModel))]
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext _context;

        public Project Project { get; set; }

        public DetailsModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Project = await _context.Projects.FindAsync(id);
            if (Project == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
