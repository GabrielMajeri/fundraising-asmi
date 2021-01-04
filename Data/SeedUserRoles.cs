using System;
using System.Threading.Tasks;
using Asmi.Fundraising.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;

namespace Asmi.Fundraising.Data
{
    using RoleManager = RoleManager<AppRole>;
    using UserManager = UserManager<AppUser>;

    public class SeedUserRoles
    {
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;
        private readonly bool _isDevelopment;

        public SeedUserRoles(RoleManager roleManager, UserManager userManager,
            IWebHostEnvironment environment)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _isDevelopment = environment.IsDevelopment();
        }

        public void Seed()
        {
            SeedAsync().Wait();
        }

        public async Task SeedAsync()
        {
            await EnsureRoleExists(AppRole.Admin);

            // Seed users unconditionally
            //if (_isDevelopment)
            {
                await EnsureUserExists(
                    "Example user",
                    "user@example.com",
                    "Test1234$"
                );

                var admin = await EnsureUserExists(
                    "Admin User",
                    "admin@example.com",
                    "Test1234$"
                );
                await _userManager.AddToRoleAsync(admin, AppRole.Admin);
            }
        }

        private async Task EnsureRoleExists(string name)
        {
            if (await _roleManager.FindByNameAsync(name) != null)
            {
                return;
            }

            var role = new AppRole { Name = name };
            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.ToString());
            }
        }

        private async Task<AppUser> EnsureUserExists(string name, string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                return user;
            }

            user = new AppUser
            {
                Email = email,
                UserName = email,
                FullName = name,
            };
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.ToString());
            }

            return user;
        }
    }
}
