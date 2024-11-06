using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UniversityManagmentSystem.Models.Context
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string UserType { get; set; }
    }
}
