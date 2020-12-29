using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Asmi.Fundraising.Models
{
    /// <summary>
    /// A contact person at a company.
    /// </summary>
    public class Contact : IValidatableObject
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }
        [BindNever]
        public Company Company { get; set; }

        [Required]
        [Display(Name = "Nume", Prompt = "Numele persoanei de contact")]
        public string Name { get; set; }

        [EmailAddress]
        [Display(Name = "Adresă de email", Prompt = "utilizator@example.com")]
        public string Email { get; set; }

        [MinLength(10)]
        [MaxLength(10)]
        [RegularExpression(@"^07[0-9]+$")]
        [Display(Name = "Număr de telefon", Prompt = "0712345678")]
        public string Telephone { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(Telephone))
            {
                yield return new ValidationResult(
                    "Câmpurile email și număr de telefon nu pot fi ambele goale"
                );
            }
        }
    }
}
