using System.Collections.Generic;
using System.Threading.Tasks;
using Asmi.Fundraising.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmartBreadcrumbs.Attributes;

namespace Asmi.Fundraising.Pages.Users
{
    [Authorize(Roles = "Administrator")]
    [Breadcrumb("Utilizatori", FromPage = typeof(Pages.IndexModel))]
    public class IndexModel : PageModel
    {
        private UserManager<AppUser> _userManager;

        public IndexModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public ICollection<AppUser> Users { get; set; }

        public async Task OnGetAsync()
        {
            Users = await _userManager.Users.ToListAsync();
        }
    }
}
