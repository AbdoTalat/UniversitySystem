using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.Marshalling;

namespace UniversityManagmentSystem.Models.Entities
{
    public class Department
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Instructor> Instructors { get; set; } = new HashSet<Instructor>();
        public virtual ICollection<Course> Courses { get; set; } = new HashSet<Course>();
        public virtual ICollection<Student> Students { get; set; } = new HashSet<Student>();
    }
}
