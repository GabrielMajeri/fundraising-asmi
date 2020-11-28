using System.ComponentModel.DataAnnotations;

namespace Asmi.Fundraising.Models
{
    public class Image
    {
        public int Id { get; set; }

        [Required]
        public byte[] Data { get; set; }

        [Required]
        public string ContentType { get; set; }

        /// Tries to determine an image's content type based on the file's extension.
        public static string ContentTypeFromFileExtension(string extension)
        {
            switch (extension)
            {
                case "png":
                    return "image/png";
                case "jpeg":
                case "jpg":
                    return "image/jpeg";
                default:
                    return null;
            }
        }
    }
}
