using System.ComponentModel.DataAnnotations;

namespace UniversityManagmentSystem.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(maximumLength: 50, MinimumLength =3)]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; } = false;
    }
}
