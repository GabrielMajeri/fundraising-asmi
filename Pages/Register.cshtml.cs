using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Asmi.Fundraising.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace Asmi.Fundraising.Pages
{
    [Breadcrumb("Înregistrare", FromPage = typeof(IndexModel))]
    public class RegisterModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        [Display(Name = "Nume complet")]
        [Required]
        public string FullName { get; set; }
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

            var user = new AppUser
            {
                Email = Email,
                UserName = Email,
                FullName = FullName,
            };

            var result = await _userManager.CreateAsync(user, Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return Page();
            }

            return RedirectToPage("Login");
        }
    }
}
