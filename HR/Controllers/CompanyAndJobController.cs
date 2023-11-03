using Microsoft.AspNetCore.Mvc;
using HR_V2.Database;

namespace HR_V2.Controllers
{
    public class CompanyAndJobController : Controller
    {
        HR_Context database;

        public CompanyAndJobController(HR_Context database)
        {
            this.database = database;
        }
        public IActionResult GetAllCompanies()
        {
            List<Company> companies = database.Companies.ToList();
            return View("all_companies",companies);
        }
       
    }
}
