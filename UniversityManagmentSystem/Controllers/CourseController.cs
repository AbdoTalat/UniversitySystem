using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using UniversityManagmentSystem.Models.Entities;
using UniversityManagmentSystem.Repository.Interfaces;
using UniversityManagmentSystem.ViewModels;

namespace UniversityManagmentSystem.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly ICourseRepository courseRepository;
        private readonly IinstructorRepository instructorRepository;
        private readonly IDepartmentRepository departmentRepository;

        public CourseController(ICourseRepository _courseRepository, IinstructorRepository _instructorRepository,
            IDepartmentRepository _departmentRepository)
        {
            courseRepository = _courseRepository;
            instructorRepository = _instructorRepository;
            departmentRepository = _departmentRepository;
        }

        public IActionResult CheckMinDegree(int MinDegree, int Degree)
        {
            if (MinDegree <= Degree / 2)
                return Json(true);
            return Json(false);
        }

        [HttpGet]
        public async Task<IActionResult> Index(string SearchString)
        {
            var Courses = await courseRepository.GetAllAsync();
            if (!String.IsNullOrEmpty(SearchString))
            {
                Courses = Courses.Where(c => c.Name.ToUpper().Contains(SearchString.ToUpper())).ToList();
            }

            return View(Courses);
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult New()
        {
            ViewData["Depts"] = departmentRepository.GetAll().ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin")]
        public IActionResult New(Course NewCourse)
        {
            if (ModelState.IsValid)
            {
                if (NewCourse.DeptID != 0)
                {
                    courseRepository.Insert(NewCourse);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "You Need To Add Department");
                }
            }
            ViewData["Depts"] = departmentRepository.GetAll().ToList();
            return View(NewCourse);
        }

        [HttpGet]
        [Authorize(Roles ="Admin, Instructor")]
        public IActionResult Details(int Id)
        {
            Course course = courseRepository.GetByID(Id);

            return View(course);
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult Edit(int Id)
        {
            Course course = courseRepository.GetByID(Id);

            ViewData["Depts"] = departmentRepository.GetAll();
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int Id, Course course)
        {
            if (ModelState.IsValid)
            {
                courseRepository.Edit(Id, course);
                return RedirectToAction("Index", "Course");
            }
            return RedirectToAction("Index", "Course");
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult Delete(int Id)
        {
            return View(courseRepository.GetByID(Id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirm(int Id)
        {
            courseRepository.Delete(Id);
            return RedirectToAction("Index", "Course");
        }
    }
}
