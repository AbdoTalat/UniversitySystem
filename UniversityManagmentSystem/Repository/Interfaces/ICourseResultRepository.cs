using UniversityManagmentSystem.Models.Entities;

namespace UniversityManagmentSystem.Repository.Interfaces
{
    public interface ICourseResultRepository
    {
        public List<CourseResult> GetAll();
        public CourseResult GetByID(int Id);
        public List<CourseResult> GetByStudentId(int StudentID);
        public void Insert(CourseResult courseResult);
        public void Edit(int Id, CourseResult courseResult);
        public void Delete(int Id);
        public List<CourseResult> TopTenGrades();
    }
}
