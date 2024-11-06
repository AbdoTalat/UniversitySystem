using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using UniversityManagmentSystem.Models.Entities;
using UniversityManagmentSystem.Repository.Interfaces;

namespace UniversityManagmentSystem.Controllers
{
    [Authorize]
    public class InstructorController : Controller
    {
        private readonly IinstructorRepository iinstructorRepository;
        private readonly IDepartmentRepository idepartmentRepository;
        private readonly ICourseRepository courseRepository;

        public InstructorController(IinstructorRepository _iinstructorRepository,
            IDepartmentRepository _idepartmentRepository, ICourseRepository _courseRepository)
        {
            iinstructorRepository = _iinstructorRepository;
            idepartmentRepository = _idepartmentRepository;
            courseRepository = _courseRepository;
        }
        public async Task<IActionResult> Index(string SearchString)
        {
            var instructors = await iinstructorRepository.GetAllAsync();
            if (!String.IsNullOrEmpty(SearchString))
            {
                instructors = instructors.Where(i => i.Name.ToUpper().Contains(SearchString.ToUpper())).ToList();
            }
            
            return View(instructors);
        }


        [HttpGet]
        [Authorize(Roles ="Admin")]
        public  IActionResult Details(int Id)
        {
            if (User.Identity.IsAuthenticated == false)
                return RedirectToAction("Unauthorized","Home");

            Instructor instructor = iinstructorRepository.GetByID(Id);

            return View(instructor);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult New()
        {
            ViewData["Depts"] = idepartmentRepository.GetAll();
            ViewData["Courses"] = courseRepository.GetAll();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult New(Instructor Newinstructor)
        {
            var instructor = iinstructorRepository.GetByEmail(Newinstructor.Email);
            if(Newinstructor.DeptID == 0 && Newinstructor.CourseID == 0)
            {
                ModelState.AddModelError("DeptID","Select Department!");
                ModelState.AddModelError("CourseID", "Select Department!");
            }
            if(instructor != null)
            {
                ModelState.AddModelError("", "This User Is Already In The System");
            }
            if (ModelState.IsValid)
            {
                iinstructorRepository.Insert(Newinstructor);
                return RedirectToAction("Index", "Movie");
            }
            ViewData["Depts"] = idepartmentRepository.GetAll();
            ViewData["Courses"] = courseRepository.GetAll();

            return View(Newinstructor);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int Id)
        {
            Instructor instructor = iinstructorRepository.GetByID(Id);

            ViewData["Depts"] = idepartmentRepository.GetAll();
            ViewData["Courses"] = courseRepository.GetAll();
            return View(instructor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int Id, Instructor instructor)
        {              
            //Instructor OldInstructor = iinstructorRepository.GetByID(Id);

            if(ModelState.IsValid)
            {
                iinstructorRepository.Edit(Id, instructor);
                return RedirectToAction("Index","Instructor");
            }
            return RedirectToAction("Index", "Instructor");
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int Id)
        {
            return View(iinstructorRepository.GetByID(Id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirm(int Id)
        {
            iinstructorRepository.Delete(Id);

            return RedirectToAction("Index","Instructor");
        }

    }
}
