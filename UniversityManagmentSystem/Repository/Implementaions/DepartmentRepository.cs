using Microsoft.EntityFrameworkCore;
using UniversityManagmentSystem.Models.Context;
using UniversityManagmentSystem.Models.Entities;
using UniversityManagmentSystem.Repository.Interfaces;

namespace UniversityManagmentSystem.Repository.Implementaions
{
    public class DepartmentRepository : IDepartmentRepository
    {
        UniversityContext context;
        public DepartmentRepository(UniversityContext _context)
        {
            context = _context;
        }

        public List<Department> GetAll()
        {
            return context.Departments.ToList();
        }
        public async Task<List<Department>> GetAllAsync()
        {
            return await context.Departments.ToListAsync();
        }
        public Department GetByID(int Id)
        {
            return context.Departments.FirstOrDefault(i => i.ID == Id);
        }
        public void Insert(Department department)
        {
            context.Departments.Add(department);
            context.SaveChanges();
        }
        public void Edit(int Id, Department department)
        {
            Department OldDepartment = GetByID(Id);
            OldDepartment.Name = department.Name;

            context.SaveChanges();
        }
        public void Delete(int Id)
        {
            Department department = GetByID(Id);
            context.Departments.Remove(department);
            context.SaveChanges();
        }
        public int CountAll()
        {
            return context.Departments.Count();
        }
    }
}
