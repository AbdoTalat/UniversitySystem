using UniversityManagmentSystem.Models.Entities;

namespace UniversityManagmentSystem.Repository.Interfaces
{
    public interface ICourseRepository
    {
        public List<Course> GetAll();
        public Task<List<Course>> GetAllAsync();
        public Course GetByID(int Id);
        public void Insert(Course course);
        public void Edit(int Id, Course course);
        public void Delete(int Id);
        public int CountAll();

        public Course GetDegrees(int Id);
    }
}
