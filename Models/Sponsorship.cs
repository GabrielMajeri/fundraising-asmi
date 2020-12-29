using System;
using System.ComponentModel.DataAnnotations;

namespace Asmi.Fundraising.Models
{
    /// <summary>
    /// Stores information about a sponsorship offered by a company
    /// for a project.
    /// </summary>
    ///
    /// The sponsorship can be facilitated by a volunteer.
    public class Sponsorship
    {
        public int Id { get; set; }

        [Required]
        public Company Company { get; set; }

        [Required]
        public Project Project { get; set; }

        /// <summary>
        /// The day on which this sponsorship's contract was signed.
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SigningDate { get; set; }

        /// <summary>
        /// The volunteer responsible for this sponsorship.
        /// </summary>
        public AppUser Volunteer { get; set; }
    }
}
