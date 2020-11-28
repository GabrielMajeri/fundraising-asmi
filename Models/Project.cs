using System.ComponentModel.DataAnnotations;

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
        public string Name { get; set; }

        [Required]
        public string Edition { get; set; }
    }
}
