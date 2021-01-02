using System.Collections.Generic;
using System.Threading.Tasks;
using Asmi.Fundraising.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;
using Asmi.Fundraising.Models;
using Microsoft.EntityFrameworkCore;

namespace Asmi.Fundraising.Pages.Projects
{
    [Breadcrumb("Proiecte", FromPage = typeof(Pages.IndexModel))]
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IEnumerable<Project> Projects { get; set; }

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Projects = await _context.Projects.ToListAsync();
        }
    }
}
