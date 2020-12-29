using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Asmi.Fundraising.Models
{
    /// <summary>
    /// A company is a legal person we interact with during the fundraising process.
    /// </summary>
    public class Company
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nume", Prompt = "Numele companiei")]
        public string Name { get; set; }

        [DataType(DataType.Url)]
        [Required]
        [Url]
        [Display(Name = "Site web", Prompt = "https://www.example.com")]
        public string Site { get; set; }

        public int? LogoId { get; set; }
        [Display(Name = "Logo")]
        public Image Logo { get; set; }

        public IEnumerable<Contact> Contacts { get; set; }

        public IEnumerable<Sponsorship> Sponsorships { get; set; }
    }
}
