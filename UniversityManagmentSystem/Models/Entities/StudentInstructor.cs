using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityManagmentSystem.Models.Entities
{
    public class StudentInstructor
    {
        public int ID { get; set; }
        [ForeignKey("Student")]
        public int StudentID { get; set; }
        public virtual Student? Student { get; set; }


        [ForeignKey("Instructor")]
        public int InstructorID { get; set; }
        public virtual Instructor? Instructor { get; set; }
    }
}
