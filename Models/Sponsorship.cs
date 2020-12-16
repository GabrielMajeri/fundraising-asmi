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

        public AppUser Volunteer { get; set; }
    }
}
