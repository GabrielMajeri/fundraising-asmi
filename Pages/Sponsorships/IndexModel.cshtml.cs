using System.Collections.Generic;
using System.Threading.Tasks;
using Asmi.Fundraising.Data;
using Asmi.Fundraising.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Asmi.Fundraising.Pages.Sponsorships
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IEnumerable<Sponsorship> Sponsorships { get; set; }

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Sponsorships = await _context.Sponsorships
                .AsNoTracking()
                .Include(s => s.Company)
                .Include(s => s.Project)
                .ToArrayAsync();
        }
    }
}
