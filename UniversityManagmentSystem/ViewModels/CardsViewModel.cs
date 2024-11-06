using UniversityManagmentSystem.Models.Entities;

namespace UniversityManagmentSystem.ViewModels
{
    public class CardsViewModel
    {
        public int InstructorsCount { get; set; }
        public int StudentsCount { get; set; }
        public int CoursesCounts { get; set; }
        public List<Student> students { get; set; }
        public List<Course> courses { get; set; }
        public List<CourseResult> coursesResults { get; set; }
    }

}
