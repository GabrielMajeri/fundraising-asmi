using System.Threading.Tasks;
using Asmi.Fundraising.Data;
using Asmi.Fundraising.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmartBreadcrumbs.Attributes;

namespace Asmi.Fundraising.Pages
{
    [Breadcrumb("ViewData.Title", FromPage = typeof(CompaniesModel))]
    public class CompanyModel : PageModel
    {
        private readonly AppDbContext _context;

        public CompanyModel(AppDbContext context)
        {
            this._context = context;
        }

        [BindProperty]
        public Company Company { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Company = await _context.Companies
                .Include(c => c.Logo)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (Company == null)
            {
                return RedirectToPage("./Companies");
            }

            return Page();
        }
    }
}
