using System.ComponentModel.DataAnnotations;

namespace UniversityManagmentSystem.ViewModels
{
    public class RemoveAdminViewModel
    {
        [Required]
        public string AdminUserName {  get; set; }
    }
}
