using Asmi.Fundraising.Data;
using Asmi.Fundraising.Models;
using Microsoft.AspNetCore.Authorization;

namespace Asmi.Fundraising.Pages.Projects
{
    [Authorize(Roles = AppRole.Admin)]
    public class DeleteModel : DeletePageModel<Models.Project>
    {
        public DeleteModel(AppDbContext context) : base(context)
        { }
    }
}
