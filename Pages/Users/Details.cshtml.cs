using System.Threading.Tasks;
using Asmi.Fundraising.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asmi.Fundraising.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;

        public DetailsModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public AppUser AppUser { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            AppUser = await _userManager.FindByIdAsync(id);
            if (AppUser == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
