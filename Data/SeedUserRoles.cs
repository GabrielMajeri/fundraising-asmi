using System;
using System.Threading.Tasks;
using Asmi.Fundraising.Models;
using Microsoft.AspNetCore.Identity;

namespace Asmi.Fundraising.Data
{
    using RoleManager = RoleManager<AppRole>;
    using UserManager = UserManager<AppUser>;

    public class SeedUserRoles
    {
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;

        public SeedUserRoles(RoleManager roleManager, UserManager userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void Seed()
        {
            SeedAsync().Wait();
        }

        public async Task SeedAsync()
        {
            await EnsureRoleExists(AppRole.Admin);
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
    }
}
