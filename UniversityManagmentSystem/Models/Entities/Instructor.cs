using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using UniversityManagmentSystem.ViewModels;

namespace UniversityManagmentSystem.Models.Entities
{
    public class Instructor
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(maximumLength: 40, MinimumLength = 3, ErrorMessage = "Name Must Be At least 3 To 40 Characters")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public decimal Salary { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [ForeignKey("Department")]
        [Required]
        public int DeptID { get; set; }

        [Required]
        [ForeignKey("Course")]
        public int CourseID { get; set; }

        public virtual Course? Course { get; set; }
        public virtual Department? Department { get; set; }
        public virtual ICollection<StudentInstructor> StudentInstructors { get; set; } = new HashSet<StudentInstructor>();

    }
}
