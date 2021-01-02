using Asmi.Fundraising.Data;

namespace Asmi.Fundraising.Pages.Sponsorships
{
    public class DeleteModel : DeletePageModel<Models.Sponsorship>
    {
        public DeleteModel(AppDbContext context) : base(context)
        { }
    }
}
