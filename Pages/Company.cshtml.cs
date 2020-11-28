using System.Threading.Tasks;
using Asmi.Fundraising.Data;
using Asmi.Fundraising.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Asmi.Fundraising.Pages
{
    [Breadcrumb("ViewData.Title", FromPage = typeof(CompaniesModel))]
    public class CompanyModel : PageModel
    {
        private readonly AppDbContext context;

        public CompanyModel(AppDbContext context)
        {
            this.context = context;
        }

        [BindProperty]
        public Company Company { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Company = await context.Companies.FindAsync(id);

            if (Company == null)
            {
                return RedirectToPage("./Companies");
            }

            return Page();
        }
    }
}
