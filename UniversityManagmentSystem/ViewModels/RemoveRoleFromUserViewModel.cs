using System.ComponentModel.DataAnnotations;

namespace UniversityManagmentSystem.ViewModels
{
    public class RemoveRoleFromUserViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
