using System.Linq;
using System.Threading.Tasks;
using Asmi.Fundraising.Data;
using Asmi.Fundraising.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Asmi.Fundraising.Pages.Companies
{
    [Breadcrumb("ViewData.Title", FromPage = typeof(IndexModel))]
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly ImageUploadService _imageUploadService;

        public EditModel(AppDbContext context, ImageUploadService imageUploadService)
        {
            this._context = context;
            this._imageUploadService = imageUploadService;
        }

        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; }
        [BindProperty]
        public Company Company { get; set; }
        [BindProperty]
        [Logo]
        public IFormFile Logo { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (Id.HasValue)
            {
                Company = await _context.Companies.FindAsync(Id.Value);
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
                return Page();
            }

            Image oldLogo = null;
            if (Id.HasValue)
            {
                Company.Id = Id.Value;
                var logoQuery = from c in _context.Companies
                                where c.Id == Company.Id
                                select c.Logo;
                oldLogo = logoQuery.FirstOrDefault();
            }

            if (Logo != null)
            {
                Company.Logo = await _imageUploadService.Upload(Logo);
            }

            _context.Update(Company);
            await _context.SaveChangesAsync();

            if (oldLogo != null)
            {
                _context.Remove(oldLogo);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Details", new { Id = Company.Id });
        }
    }
}
