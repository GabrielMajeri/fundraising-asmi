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
        public string Name { get; set; }

        [DataType(DataType.Url)]
        [Required]
        public string Site { get; set; }

        public Image Logo { get; set; }
    }
}
