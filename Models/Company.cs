using System.ComponentModel.DataAnnotations;

namespace Asmi.Fundraising.Models
{
    public class Company
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
