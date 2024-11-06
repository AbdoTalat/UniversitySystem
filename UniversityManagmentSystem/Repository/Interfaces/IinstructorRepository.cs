using UniversityManagmentSystem.Models.Entities;

namespace UniversityManagmentSystem.Repository.Interfaces
{
    public interface IinstructorRepository
    {
        public Task<List<Instructor>> GetAllAsync();
        public List<Instructor> GetAll();
        public Instructor GetByID(int Id);
        public Instructor GetByEmail(string email);
        public Task<Instructor> GetByEmailAsync(string enail);
        public void Insert(Instructor instructor);
        public void Edit(int Id, Instructor instructor);
        public void Delete(int Id);
        public int CountAll();
    }
}
