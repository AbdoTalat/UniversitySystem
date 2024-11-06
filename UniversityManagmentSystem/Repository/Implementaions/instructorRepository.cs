using Microsoft.EntityFrameworkCore;
using UniversityManagmentSystem.Models.Context;
using UniversityManagmentSystem.Models.Entities;
using UniversityManagmentSystem.Repository.Interfaces;

namespace UniversityManagmentSystem.Repository.Implementaions
{
    public class instructorRepository : IinstructorRepository
    {
        UniversityContext context;
        public instructorRepository(UniversityContext _context)
        {
            context = _context;
        }

        public async Task<List<Instructor>> GetAllAsync()
        {
            return await context.Instructors.ToListAsync();
        }

        public List<Instructor> GetAll()
        {
            return context.Instructors.ToList();
        }
        public Instructor GetByID(int Id)
        {
            return context.Instructors.Include(i => i.Department).Include(i => i.Course).FirstOrDefault(i => i.ID == Id);
        }

        public Instructor GetByEmail(string Email)
        {
            return context.Instructors.FirstOrDefault(i => i.Email == Email);
        }

        public async Task<Instructor> GetByEmailAsync(string Email)
        {
            return await context.Instructors.FirstOrDefaultAsync(i => i.Email == Email);
        }

        public void Insert(Instructor instructor)
        {
            context.Instructors.Add(instructor);
            context.SaveChanges();
        }
        public void Edit(int Id, Instructor instructor)
        {
            Instructor OldInstructor = GetByID(Id);
            OldInstructor.Name = instructor.Name;
            OldInstructor.Email = instructor.Email;
            OldInstructor.Salary = instructor.Salary;
            OldInstructor.Address = instructor.Address;
            OldInstructor.Phone = instructor.Phone;
            OldInstructor.DeptID = instructor.DeptID;
            OldInstructor.CourseID = instructor.CourseID;

            context.SaveChanges();
        }

        public void Delete(int Id)
        {
            Instructor instructor = GetByID(Id);
            context.Instructors.Remove(instructor);
            context.SaveChanges();
        }

        public int CountAll()
        {
            return context.Instructors.Count();
        }


    }
}
