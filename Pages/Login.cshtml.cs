using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Asmi.Fundraising.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Asmi.Fundraising.Pages
{
    [Breadcrumb("Logare", FromPage = typeof(IndexModel))]
    public class LoginModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public LoginModel(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        [Display(Name = "Email")]
        [Required]
        public string Email { get; set; }
        [BindProperty]
        [Display(Name = "Parolă")]
        [Required]
        public string Password { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                ModelState.AddModelError("", "User not found");
                return Page();
            }

            var result = await _signInManager.PasswordSignInAsync(user, Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid password");
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}
