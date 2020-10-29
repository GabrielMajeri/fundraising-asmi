using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Asmi.Fundraising.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Asmi.Fundraising.Pages
{
    public class CompaniesModel : PageModel
    {
        private AppDbContext context;

        public IList<Company> Companies { get; set; }

        public CompaniesModel(AppDbContext context)
        {
            this.context = context;
        }

        public async Task OnGetAsync()
        {
            var companies = from c in context.Companies
                            orderby c.Name
                            select c;
            Companies = await companies.ToListAsync();
        }
    }
}
