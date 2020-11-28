using System.Threading.Tasks;
using Asmi.Fundraising.Data;
using Microsoft.AspNetCore.Mvc;

namespace Asmi.Fundraising.Controllers
{
    /// <summary>
    /// Provides access to the image store.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ImageController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var image = await _context.Images.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }

            return File(image.Data, image.ContentType);
        }
    }
}
