using Asmi.Fundraising.Data;

namespace Asmi.Fundraising.Pages.Projects
{
    public class DeleteModel : DeletePageModel<Models.Project>
    {
        public DeleteModel(AppDbContext context) : base(context)
        { }
    }
}
