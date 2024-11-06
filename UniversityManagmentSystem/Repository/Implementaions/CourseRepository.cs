using Microsoft.EntityFrameworkCore;
using UniversityManagmentSystem.Models.Context;
using UniversityManagmentSystem.Models.Entities;
using UniversityManagmentSystem.Repository.Interfaces;

namespace UniversityManagmentSystem.Repository.Implementaions
{
    public class CourseRepository : ICourseRepository
    {
        UniversityContext context;
        public CourseRepository(UniversityContext _context)
        {
            context = _context;
        }

        public List<Course> GetAll()
        {
            return context.Courses.Include(c => c.Department).ToList();
        }

        public async Task<List<Course>> GetAllAsync()
        {
            return await context.Courses.Include(c => c.Department).Include(c => c.Instructors).ToListAsync();
        }
        public Course GetByID(int Id)
        {
            return context.Courses.Include(c => c.Department).FirstOrDefault(i => i.ID == Id);
        }
        public void Insert(Course course)
        {
            context.Courses.Add(course);
            context.SaveChanges();
        }
        public void Edit(int Id, Course course)
        {
            Course OldCourse = GetByID(Id);

            OldCourse.Name = course.Name;
            OldCourse.Hours = course.Hours;
            OldCourse.Degree = course.Degree;
            OldCourse.MinDegree = course.MinDegree;
            OldCourse.DeptID = course.DeptID;

            context.SaveChanges();
        }
        public void Delete(int Id)
        {
            Course course = GetByID(Id);
            context.Courses.Remove(course);
            context.SaveChanges();
        }
        public int CountAll()
        {
            return context.Courses.Count();
        }


        public Course GetDegrees(int Id)
        {
            var course = context.Courses
                .Include(c => c.CourseResults)
                .FirstOrDefault(s => s.ID == Id);

            return course;
        }
    }
}
