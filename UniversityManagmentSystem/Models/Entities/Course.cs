using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.Marshalling;
using UniversityManagmentSystem.ViewModels;

namespace UniversityManagmentSystem.Models.Entities
{
    public class Course
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(5, 80)]
        public int Hours { get; set; }

        [Required]
        [Range(60, 120)]
        public int Degree { get; set; }

        [Required]
        [Range(30, 60)]
        [Remote(action: "CheckMinDegree", controller: "Course", AdditionalFields = "Degree", ErrorMessage = "Min Degree Can't Be More Than Half Of The Total Degree")]
        public int MinDegree { get; set; }

        [Required]
        [ForeignKey("Department")]
        public int DeptID { get; set; }

        public virtual Department? Department { get; set; }

        public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new HashSet<StudentCourse>();
        public virtual ICollection<CourseResult> CourseResults { get; set; } = new HashSet<CourseResult>();
        public virtual ICollection<Instructor> Instructors { get; set; } = new HashSet<Instructor>();
    }
}
