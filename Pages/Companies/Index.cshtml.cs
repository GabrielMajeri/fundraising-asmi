using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Asmi.Fundraising.Data;
using Asmi.Fundraising.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmartBreadcrumbs.Attributes;

namespace Asmi.Fundraising.Pages.Companies
{
    [Breadcrumb("Companii", FromPage = typeof(Pages.IndexModel))]
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IList<Company> Companies { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; }
        public bool HasSearchQuery { get => !string.IsNullOrEmpty(SearchQuery); }

        public IndexModel(AppDbContext context)
        {
            this._context = context;
        }

        public async Task OnGetAsync()
        {
            var companies = from c in _context.Companies
                            select c;

            if (HasSearchQuery)
            {
                // Apply the search query
                companies = companies.Where(c => c.Name.ToLower().Contains(SearchQuery.ToLower()));
            }

            // Order alphabetically by name
            companies = companies.OrderBy(c => c.Name);

            Companies = await companies.ToListAsync();
        }
    }
}
