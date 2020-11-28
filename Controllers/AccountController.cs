using System.Security.Claims;
using System.Threading.Tasks;
using Asmi.Fundraising.Models;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Asmi.Fundraising.Controllers
{
    /// <summary>
    /// Provides authentication using the <c>@asmi.ro</c> account.
    /// </summary>
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet("Login")]
        public IActionResult Login(string returnUrl = "/")
        {
            string redirectUrl = Url.Action(nameof(LoginResponse), new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("LoginResponse")]
        public async Task<IActionResult> LoginResponse(string returnUrl)
        {
            // Retrieve the user info returned by Google login
            var info = await _signInManager.GetExternalLoginInfoAsync();

            // User did not sign in, redirect them back to the login screen
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            // Try to sign in using an existing user, if possible
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);

            if (result.Succeeded)
            {
                return Redirect(returnUrl);
            }

            // Otherwise, create a new user
            var user = new AppUser
            {
                Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                UserName = info.Principal.FindFirst(ClaimTypes.Email).Value
            };

            var identResult = await _userManager.CreateAsync(user);
            if (identResult.Succeeded)
            {
                identResult = await _userManager.AddLoginAsync(user, info);
                if (identResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return Redirect(returnUrl);
                }
            }

            return BadRequest();
        }
    }
}
