using UniversityManagmentSystem.Models.Entities;

namespace UniversityManagmentSystem.Repository.Interfaces
{
    public interface IDepartmentRepository
    {
        public List<Department> GetAll();
        public Task<List<Department>> GetAllAsync();
        public Department GetByID(int Id);
        public void Insert(Department department);
        public void Edit(int Id, Department department);
        public void Delete(int Id);
        public int CountAll();
    }
}
