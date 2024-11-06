using System.ComponentModel.DataAnnotations;

namespace UniversityManagmentSystem.ViewModels
{
    public class EditProfileViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string UserType {  get; set; }

    }
}
