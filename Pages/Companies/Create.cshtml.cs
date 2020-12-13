using System.Threading.Tasks;
using Asmi.Fundraising.Data;
using Asmi.Fundraising.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Asmi.Fundraising.Pages.Companies
{
    [Breadcrumb("ViewData.Title", FromPage = typeof(IndexModel))]
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        [BindProperty]
        public Company Company { get; set; }

        public CreateModel(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _context.Companies.AddAsync(Company);
            await _context.SaveChangesAsync();

            return RedirectToPage("Details", new { Id = Company.Id });
        }
    }
}
