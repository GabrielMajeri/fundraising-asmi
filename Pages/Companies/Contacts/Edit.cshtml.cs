using System.Threading.Tasks;
using Asmi.Fundraising.Data;
using Asmi.Fundraising.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmartBreadcrumbs.Attributes;

namespace Asmi.Fundraising.Pages.Companies.Contacts
{
    [Breadcrumb("ViewData.Title", FromPage = typeof(Companies.IndexModel))]
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int? CompanyId { get; set; }
        public Company Company { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; }
        [BindProperty]
        public Contact Contact { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Determine if we're editing an existing contact or creating a new one.
            if (Id.HasValue)
            {
                Contact = await _context.Contacts
                    .Include(c => c.Company)
                    .FirstOrDefaultAsync(c => c.Id == Id.Value);

                if (Contact == null)
                {
                    return NotFound();
                }

                Company = Contact.Company;
            }
            else
            {
                // When creating, we need to know the contact's company
                if (!CompanyId.HasValue)
                {
                    return NotFound();
                }

                Company = await _context.Companies.FindAsync(CompanyId.Value);
                if (Company == null)
                {
                    return NotFound();
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return await OnGetAsync();
            }

            if (Id.HasValue)
            {
                Contact.Id = Id.Value;
            }

            _context.Update(Contact);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Companies/Details", new { Id = Contact.CompanyId });
        }
    }
}
