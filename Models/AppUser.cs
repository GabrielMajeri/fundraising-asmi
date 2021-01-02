using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Asmi.Fundraising.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
    }
}
