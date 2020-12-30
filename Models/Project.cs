using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asmi.Fundraising.Models
{
    /// <summary>
    /// A project is an activity with clear objectives, for which we fundraise.
    /// </summary>
    ///
    /// Every project is led by one or more project managers, who are
    /// responsible for defining the project's vision and delegating its
    /// implementation to volunteers.
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nume")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Ediție")]
        public string Edition { get; set; }

        [NotMapped]
        public string FullName { get => $"{Name} {Edition}"; }

        public int? LogoId { get; set; }
        public Image Logo { get; set; }

        public IEnumerable<Sponsorship> Sponsorships { get; set; }
    }
}
