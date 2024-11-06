using UniversityManagmentSystem.Models.Entities;
using UniversityManagmentSystem.ViewModels;

namespace UniversityManagmentSystem.Repository.Interfaces
{
    public interface IStudentRepository
    {
        public Task<List<Student>> GetAllAsync();
        public List<Student> GetAll();
        public Student GetByID(int Id);
        public Student GetDegrees(int Id);
        public Student GetByEmail(string Email);
        public Task<Student> GetByEmailAsync(string Email);
        public void Insert(Student student);
        public void Edit(int Id, Student student);
        public void Delete(int Id);
        public int CountAll();
    }
}
