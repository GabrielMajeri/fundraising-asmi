using System.Threading.Tasks;
using Asmi.Fundraising.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asmi.Fundraising.Pages
{
    public abstract class DeletePageModel<TEntity> : PageModel
        where TEntity : class
    {
        private readonly AppDbContext _context;

        public DeletePageModel(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var set = _context.Set<TEntity>();

            var entity = await set.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            set.Remove(entity);
            await _context.SaveChangesAsync();

            return RedirectToPagePermanent("Index");
        }
    }
}
