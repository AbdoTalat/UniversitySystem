using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityManagmentSystem.Repository.Interfaces;
using UniversityManagmentSystem.ViewModels;

namespace UniversityManagmentSystem.Controllers
{
    public class DashboardController : Controller
    {
        IinstructorRepository iinstructorRepository;
        IStudentRepository studentRepository;
        ICourseRepository courseRepository;
        ICourseResultRepository courseResultRepository;
        public DashboardController(IinstructorRepository _iinstructorRepository, 
            IStudentRepository _studentRepository, ICourseRepository _courseRepository,
            ICourseResultRepository _courseResultRepository)
        {
            iinstructorRepository = _iinstructorRepository;
            studentRepository = _studentRepository;
            courseRepository = _courseRepository;
            courseResultRepository = _courseResultRepository;
        }


        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            CardsViewModel cardsViewModel = new CardsViewModel();
            cardsViewModel.InstructorsCount = iinstructorRepository.CountAll();
            cardsViewModel.StudentsCount = studentRepository.CountAll();
            cardsViewModel.CoursesCounts = courseRepository.CountAll();

            cardsViewModel.students = studentRepository.GetAll();
            cardsViewModel.courses = courseRepository.GetAll();
            cardsViewModel.coursesResults = courseResultRepository.TopTenGrades();
            

            return View(cardsViewModel);
        }

        [HttpGet]
        public IActionResult Welcome()
        {
            return View();
        }


    }
}
