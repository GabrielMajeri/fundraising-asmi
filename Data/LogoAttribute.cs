using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Asmi.Fundraising.Data
{
    public class LogoAttribute : ValidationAttribute
    {
        // Maximum allowed size of an uploaded image.
        public const long MaxFileSizeBytes = 512 * 1024;

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var file = (IFormFile)value;

            if (file.Length >= MaxFileSizeBytes)
            {
                return new ValidationResult("Image is too large");
            }

            if (file.ContentType != "image/png" && file.ContentType != "image/jpeg")
            {
                return new ValidationResult("Unsupported image format");
            }

            return ValidationResult.Success;
        }
    }
}
