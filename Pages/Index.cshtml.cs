using System.Threading.Tasks;
using Asmi.Fundraising.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Asmi.Fundraising.Pages
{
    [DefaultBreadcrumb("Acasă")]
    public class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;

        public IndexModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public AppUser AppUser { get; set; }
        public bool UserIsAdmin { get; set; }

        public async Task OnGetAsync()
        {
            var result = await HttpContext.AuthenticateAsync();
            if (result.Succeeded)
            {
                AppUser = await _userManager.GetUserAsync(User);
                UserIsAdmin = User.IsInRole(AppRole.Admin);
            }
        }
    }
}
