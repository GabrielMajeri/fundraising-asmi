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
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly ImageUploadService _imageUploadService;

        [BindProperty]
        public Project Project { get; set; }
        [BindProperty]
        public IFormFile Logo { get; set; }

        public CreateModel(AppDbContext context, ImageUploadService imageUploadService)
        {
            this._context = context;
            this._imageUploadService = imageUploadService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (Logo != null)
            {
                Project.Logo = await _imageUploadService.Upload(Logo);
            }

            await _context.Projects.AddAsync(Project);
            await _context.SaveChangesAsync();

            return RedirectToPage("Details", new { Id = Project.Id });
        }
    }
}
