using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniversityManagmentSystem.Models.Entities;

namespace UniversityManagmentSystem.Models.Context
{
    public class UniversityContext : IdentityDbContext<ApplicationUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public UniversityContext()
        {

        }

        public UniversityContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<StudentCourse>()
            //    .HasKey(cs => new { cs.CourseID, cs.StudentID });

            modelBuilder.Entity<StudentCourse>()
                .HasOne(s => s.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentID);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseID);


            //modelBuilder.Entity<StudentInstructor>()
            //    .HasKey(si => new {si.StudentID, si.InstructorID});

            modelBuilder.Entity<StudentInstructor>()
                .HasOne(si => si.Student)
                .WithMany(s => s.StudentInstructors)
                .HasForeignKey(si => si.StudentID);

            modelBuilder.Entity<StudentInstructor>()
                .HasOne(si => si.Instructor)
                .WithMany(i => i.StudentInstructors)
                .HasForeignKey(si => si.InstructorID);

            base.OnModelCreating(modelBuilder);
        }


        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<StudentCourse> StudentsCourses { get; set; }
        public virtual DbSet<StudentInstructor> StudentsInstructors { get; set; }
        public virtual DbSet<CourseResult> CoursesResults { get; set; }
    }
}
