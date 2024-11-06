using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UniversityManagmentSystem.Models.Entities
{
    public class Student
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(maximumLength: 40, MinimumLength = 3, ErrorMessage = "Name Must Be At least 3 To 40 Characters")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Address { get; set; }

        [ForeignKey("Department")]
        [Required]
        public int DeptID { get; set; }

        public virtual Department? Department { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new HashSet<StudentCourse>();
        public virtual ICollection<StudentInstructor> StudentInstructors { get; set; } = new HashSet<StudentInstructor>();
        public virtual ICollection<CourseResult> CoursesResults { get; set; } = new HashSet<CourseResult>();
    }
}
