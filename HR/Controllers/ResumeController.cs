using Microsoft.AspNetCore.Mvc;
using HR_V2.Database;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HR_V2.Controllers
{
    public class ResumeController : Controller
    {
        HR_Context database;

        public ResumeController(HR_Context database)
        {
            this.database = database;

        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddResume()
        {
            var client_id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return View("add_resume_form");
        }
        [HttpPost]
        public IActionResult AddResume(Resume resume)
        {
            return Content(resume.ResumeKeySkills);
        }

        [HttpGet]
        public IActionResult GetAllResume()
        {
            return View("get_all_resumes");
        }
    }
}
