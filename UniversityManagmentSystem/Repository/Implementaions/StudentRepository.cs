using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using UniversityManagmentSystem.Models.Context;
using UniversityManagmentSystem.Models.Entities;
using UniversityManagmentSystem.Repository.Interfaces;
using UniversityManagmentSystem.ViewModels;

namespace UniversityManagmentSystem.Repository.Implementaions
{
    public class StudentRepository : IStudentRepository
    {
        UniversityContext context;
        public StudentRepository(UniversityContext _context)
        {
            context = _context;
        }



        public async Task<List<Student>> GetAllAsync()
        {
            return await context.Students.ToListAsync();
        }
        public List<Student> GetAll()
        {
            return context.Students.ToList();
        }
        public Student GetByID(int Id)
        {
            return context.Students.Include(i => i.Department).FirstOrDefault(i => i.ID == Id);
        }
        public Student GetByEmail(string Email)
        {
            return context.Students.FirstOrDefault(s => s.Email == Email);
        }
        public async Task<Student> GetByEmailAsync(string Email)
        {
            return await context.Students.FirstOrDefaultAsync(s => s.Email == Email);
        }
        public void Insert(Student student)
        {
            context.Students.Add(student);
            context.SaveChanges();
        }
        public void Edit(int Id, Student student)
        {
            Student OldStudent = GetByID(Id);

            OldStudent.Name = student.Name;
            OldStudent.Email = student.Email;
            OldStudent.Address = student.Address;
            OldStudent.DeptID = student.DeptID;

            context.SaveChanges();
        }
        public void Delete(int Id)
        {
            Student student = GetByID(Id);
            context.Students.Remove(student);
            context.SaveChanges();
        }
        public int CountAll()
        {
            return context.Students.Count();
        }


        public Student GetDegrees(int Id)
        {
            var student = context.Students
                .Include(s => s.CoursesResults)
                .Include(s => s.StudentCourses)
                .ThenInclude(sc => sc.Course)
                .FirstOrDefault(s => s.ID == Id);

            return student;
        }

    }

}
