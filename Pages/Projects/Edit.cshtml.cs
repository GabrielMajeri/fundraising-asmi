using System.Linq;
using System.Threading.Tasks;
using Asmi.Fundraising.Data;
using Asmi.Fundraising.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Asmi.Fundraising.Pages.Projects
{
    [Breadcrumb("ViewData.Title", FromPage = typeof(IndexModel))]
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly ImageUploadService _imageUploadService;

        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; }
        [BindProperty]
        public Project Project { get; set; }
        [BindProperty]
        [Logo]
        public IFormFile Logo { get; set; }

        public EditModel(AppDbContext context, ImageUploadService imageUploadService)
        {
            this._context = context;
            this._imageUploadService = imageUploadService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            // If editing a project
            if (Id.HasValue)
            {
                // Load the existing data
                Project = await _context.Projects.FindAsync(Id.Value);
                if (Project == null)
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
                Project.Id = Id.Value;
                var logoQuery = from p in _context.Projects
                                where p.Id == Project.Id
                                select p.Logo;
                oldLogo = logoQuery.FirstOrDefault();
            }

            if (Logo != null)
            {
                Project.Logo = await _imageUploadService.Upload(Logo);
            }

            _context.Update(Project);
            await _context.SaveChangesAsync();

            if (oldLogo != null)
            {
                _context.Remove(oldLogo);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}
