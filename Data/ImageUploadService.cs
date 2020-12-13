using System.IO;
using System.Threading.Tasks;
using Asmi.Fundraising.Models;
using Microsoft.AspNetCore.Http;

namespace Asmi.Fundraising.Data
{
    public class ImageUploadService
    {
        private readonly AppDbContext _context;

        public ImageUploadService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Image> Upload(IFormFile file)
        {
            using var memoryStream = new MemoryStream();

            await file.CopyToAsync(memoryStream);

            var image = new Image
            {
                Data = memoryStream.ToArray(),
                ContentType = file.ContentType,
            };

            await _context.Images.AddAsync(image);
            await _context.SaveChangesAsync();

            return image;
        }
    }
}
