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

        public bool UserIsAdmin { get; set; }

        public AppUser AppUser { get; set; }
        public bool AppUserIsAdmin { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            UserIsAdmin = User.IsInRole(AppRole.Admin);

            AppUser = await _userManager.FindByIdAsync(id);
            if (AppUser == null)
            {
                return NotFound();
            }

            AppUserIsAdmin = await _userManager.IsInRoleAsync(AppUser, AppRole.Admin);

            return Page();
        }

        public async Task<IActionResult> OnPostRemoveRoleAsync(string id)
        {
            if (!User.IsInRole(AppRole.Admin))
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.RemoveFromRoleAsync(user, AppRole.Admin);
            if (!result.Succeeded)
            {
                return BadRequest();
            }

            return RedirectToPage();
        }
    }
}
