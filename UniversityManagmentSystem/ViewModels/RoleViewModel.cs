using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace UniversityManagmentSystem.ViewModels
{
    public class RoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
