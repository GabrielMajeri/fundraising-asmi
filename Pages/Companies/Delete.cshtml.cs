using Asmi.Fundraising.Data;
using Asmi.Fundraising.Models;
using Microsoft.AspNetCore.Authorization;

namespace Asmi.Fundraising.Pages.Companies
{
    [Authorize(Roles = AppRole.Admin)]
    public class DeleteModel : DeletePageModel<Models.Company>
    {
        public DeleteModel(AppDbContext context)
            : base(context)
        { }
    }
}
