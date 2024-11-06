using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using UniversityManagmentSystem.Models;
using UniversityManagmentSystem.Repository.Interfaces;

namespace UniversityManagmentSystem.Controllers
{
    public class CourseResultController : Controller
    {
        private readonly ICourseResultRepository courseResultRepository;
        private readonly IStudentRepository studentRepository;
        private readonly ICourseRepository courseRepository;

        public CourseResultController(ICourseResultRepository _courseResultRepository,
            IStudentRepository _studentRepository,
            ICourseRepository _courseRepository)
        {
            courseResultRepository = _courseResultRepository;
            studentRepository = _studentRepository;
            courseRepository = _courseRepository;
        }

        [HttpGet]
        public ActionResult Editdegree(int CourseResultID)
        {
            var Result = courseResultRepository.GetByID(CourseResultID);
            return View(Result);
        }


        
    }
}
