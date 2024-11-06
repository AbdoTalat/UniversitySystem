using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace UniversityManagmentSystem.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(maximumLength:50, MinimumLength =3)]
        public string UserName {  get; set; }

        [Required]
        [EmailAddress]
        public string Email {  get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password {  get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

         [Required]
        public string UserType { get; set; }

    }
}
