using Microsoft.EntityFrameworkCore;
using UniversityManagmentSystem.Models.Context;
using UniversityManagmentSystem.Models.Entities;
using UniversityManagmentSystem.Repository.Interfaces;

namespace UniversityManagmentSystem.Repository.Implementaions
{
    public class CourseResultRepository : ICourseResultRepository
    {
        UniversityContext context;
        public CourseResultRepository(UniversityContext _context)
        {
            context = _context;
        }
        public List<CourseResult> GetAll()
        {
            return context.CoursesResults.ToList();
        }

        public List<CourseResult> GetByStudentId(int studentID)
        {
            var Result = context.CoursesResults
                .Include(sr => sr.Student)
                .Include(sr => sr.Course)
                .Where(sr => sr.StudentID == studentID).ToList();
            return Result;
        }
        public CourseResult GetByID(int Id)
        {
            return context.CoursesResults.FirstOrDefault(i => i.ID == Id);
        }

        public List<CourseResult> GetByStdID(int StudentID)
        {
            var Result = context.CoursesResults
                .Where(cr => cr.StudentID == StudentID)
                .ToList();

            return Result;
        }

        public void Insert(CourseResult courseResult)
        {
            context.CoursesResults.Add(courseResult);
            context.SaveChanges();
        }
        public void Edit(int Id, CourseResult courseResult)
        {
            CourseResult OldCourseResult = GetByID(Id);

            OldCourseResult.Grade = courseResult.Grade;
            OldCourseResult.CourseID = courseResult.CourseID;

            context.SaveChanges();
        }
        public void Delete(int Id)
        {
            CourseResult courseResult = GetByID(Id);
            context.CoursesResults.Remove(courseResult);
            context.SaveChanges();
        }

        public List<CourseResult> TopTenGrades()
        {
            return context.CoursesResults.OrderByDescending(cr => cr.Grade)
                .Take(10).ToList();
        }
    }
}
