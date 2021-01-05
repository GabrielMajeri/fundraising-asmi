using Asmi.Fundraising.Data;
using Asmi.Fundraising.Models;
using Microsoft.AspNetCore.Authorization;

namespace Asmi.Fundraising.Pages.Sponsorships
{
    [Authorize(Roles = AppRole.Admin)]
    public class DeleteModel : DeletePageModel<Models.Sponsorship>
    {
        public DeleteModel(AppDbContext context) : base(context)
        { }
    }
}
