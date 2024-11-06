using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityManagmentSystem.Models.Entities
{
    public class StudentCourse
    {
        public int ID { get; set; }
        [ForeignKey("Course")]
        public int CourseID { get; set; }
        public virtual Course? Course { get; set; }


        [ForeignKey("Student")]
        public int StudentID { get; set; }
        public virtual Student? Student { get; set; }
    }
}
