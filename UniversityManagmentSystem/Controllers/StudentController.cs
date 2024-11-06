using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityManagmentSystem.Models.Entities;
using UniversityManagmentSystem.Repository.Interfaces;
using UniversityManagmentSystem.ViewModels;

namespace UniversityManagmentSystem.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly IinstructorRepository iinstructorRepository;
        private readonly IDepartmentRepository idepartmentRepository;
        private readonly ICourseRepository courseRepository;
        private readonly IStudentRepository studentRepository;
        private readonly ICourseResultRepository courseResultRepository;

        public StudentController(IinstructorRepository _iinstructorRepository,
            IDepartmentRepository _idepartmentRepository,
            ICourseRepository _courseRepository,
            IStudentRepository _studentRepository,
            ICourseResultRepository _courseResultRepository)
        {
            iinstructorRepository = _iinstructorRepository;
            idepartmentRepository = _idepartmentRepository;
            courseRepository = _courseRepository;
            studentRepository = _studentRepository;
            courseResultRepository = _courseResultRepository;
        }

        public async Task<IActionResult> Index(string SearchString)
        {
            var students = await studentRepository.GetAllAsync();
            if (!String.IsNullOrEmpty(SearchString))
            {
                students = students.Where(i => i.Name.ToUpper().Contains(SearchString.ToUpper())).ToList();
            }

            return View(students);
        }


        [HttpGet]
        [Authorize(Roles ="Admin, Instructor")]
        public IActionResult Details(int Id)
        {
            Student student = studentRepository.GetByID(Id);

            return View(student);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult New()
        {
            ViewData["Depts"] = idepartmentRepository.GetAll().ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult New(Student NewStudent)
        {
            if (ModelState.IsValid)
            {
                if (NewStudent.DeptID != 0)
                {
                    studentRepository.Insert(NewStudent);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("DeptID", "Select Department!");
                }
            }
            ViewData["Depts"] = idepartmentRepository.GetAll().ToList();

            return View(NewStudent);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int Id)
        {
            Student student = studentRepository.GetByID(Id);

            ViewData["Depts"] = idepartmentRepository.GetAll();
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int Id, Student student)
        {
            //Instructor OldInstructor = iinstructorRepository.GetByID(Id);

            if (ModelState.IsValid)
            {
                studentRepository.Edit(Id, student);
                return RedirectToAction("Index", "Student");
            }
            return RedirectToAction("Index", "Student");
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int Id)
        {
            return View(studentRepository.GetByID(Id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirm(int Id)
        {
            studentRepository.Delete(Id);

            return RedirectToAction("Index", "Student");
        }

        [HttpGet]
        public IActionResult Degrees(int Id)
        {
            var Result = studentRepository.GetDegrees(Id);
            return View(Result);
        }
        
    }
}
