using System.ComponentModel.DataAnnotations;

namespace Asmi.Fundraising.Models
{
    /// <summary>
    /// A contact person at a company.
    /// </summary>
    public class Contact
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        [Required]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [MinLength(10)]
        [MaxLength(10)]
        [RegularExpression(@"^07[0-9]+$")]
        public string Telephone { get; set; }
    }
}
