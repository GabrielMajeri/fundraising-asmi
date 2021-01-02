using System.Threading.Tasks;
using Asmi.Fundraising.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Asmi.Fundraising.Pages
{
    [DefaultBreadcrumb("Acasă")]
    public class IndexModel : PageModel
    {
        public bool UserIsAdmin { get; set; }

        public async Task OnGetAsync()
        {
            var result = await HttpContext.AuthenticateAsync();
            if (result.Succeeded)
            {
                UserIsAdmin = result.Principal.IsInRole(AppRole.Admin);
            }
        }
    }
}
