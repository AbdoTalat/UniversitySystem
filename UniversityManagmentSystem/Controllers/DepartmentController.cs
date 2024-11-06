using Microsoft.AspNetCore.Mvc;
using UniversityManagmentSystem.Repository.Interfaces;

namespace UniversityManagmentSystem.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository departmentRepository;

        public DepartmentController(IDepartmentRepository _departmentRepository)
        {
            departmentRepository = _departmentRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string SearchString)
        {
            var Courses = await departmentRepository.GetAllAsync();
            if (!String.IsNullOrEmpty(SearchString))
            {
                Courses = Courses.Where(c => c.Name.ToUpper().Contains(SearchString.ToUpper())).ToList();
            }

            return View(Courses);
        }
    }
}
