using HR_V2.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_V2.Controllers
{
    public class apiController : Controller
    {
        [HttpGet]
        public IActionResult GetAllClients()
        {
            return StatusCode(200);
        }
        [HttpGet]
        public IActionResult GetClientById(int clientId)
        {
            return StatusCode(200);
        }
        [HttpGet]
        public IActionResult AddClient(string name, string surname, string email, string password)
        {
            var client = new Client
            {
                ClientName = name,
                ClientSurname = surname,
                ClientEmail = email,
                ClientPassword = password
            };
            return StatusCode(200);
        }
    }
}
