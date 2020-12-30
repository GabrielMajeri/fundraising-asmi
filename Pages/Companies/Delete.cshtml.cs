using Asmi.Fundraising.Data;

namespace Asmi.Fundraising.Pages.Companies
{
    public class DeleteModel : DeletePageModel<Models.Company>
    {
        public DeleteModel(AppDbContext context)
            : base(context)
        { }
    }
}
