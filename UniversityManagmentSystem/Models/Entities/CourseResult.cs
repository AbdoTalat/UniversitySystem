using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityManagmentSystem.Models.Entities
{
    public class CourseResult
    {
        public int ID { get; set; }
        public float Grade { get; set; }

        [ForeignKey("Srudent")]
        public int StudentID { get; set; }


        [ForeignKey("CourseID")]
        public int CourseID { get; set; }

        public virtual Student? Student { get; set; }
        public virtual Course? Course { get; set; }
    }
}
