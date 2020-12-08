using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Asmi.Fundraising.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmartBreadcrumbs.Attributes;

namespace Asmi.Fundraising.Pages
{
    [Breadcrumb("Proiecte")]
    public class ProjectsModel : PageModel
    {
        private readonly AppDbContext _context;

        public class ProjectData
        {
            public string FullName;
            public int? LogoId;
        }

        public IList<ProjectData> Projects { get; set; }

        public ProjectsModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            var query = from p in _context.Projects
                        select new ProjectData
                        {
                            FullName = $"{p.Name} {p.Edition}",
                            LogoId = p.Logo != null ? p.Logo.Id : null,
                        };
            Projects = await query.ToListAsync();
        }
    }
}
