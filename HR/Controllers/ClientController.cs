using HR_V2.Database;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;

namespace HR_V2.Controllers
{
    public class ClientController : Controller
    {
        HR_Context database;
        IAuthorizationService authorization_service;
        IAuthenticationService authentication_service;

        public ClientController(HR_Context database, IAuthorizationService authorization_service, IAuthenticationService authentication_service)
        {
            this.database = database;
            this.authorization_service = authorization_service;
            this.authentication_service = authentication_service;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View("registration_form");
        }

        [HttpPost]
        public async Task<ActionResult> Registration(Client client)
        {
            using var db = database;

            try
            {
                await database.Database.OpenConnectionAsync();
                await database.AddAsync(client);
                await database.SaveChangesAsync();
                await database.Database.CloseConnectionAsync();
            }
            catch (Exception ex)
            {
                // логгировать ошибку, если регистрация не удалась;
            }

            string? email = client.ClientEmail;
            var currentClient = database.Clients.FirstOrDefault(cl => cl.ClientEmail == client.ClientEmail);

            List<Claim> claimsList = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, currentClient.ClientId.ToString()),
                new Claim(ClaimTypes.Email, currentClient.ClientEmail),
                new Claim(ClaimTypes.Name, currentClient.ClientName),
                new Claim(ClaimTypes.Surname, currentClient.ClientSurname),

            };

            var claimsIdentity = new ClaimsIdentity(claimsList, "Cookies");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return Redirect("~/Client/Index");
        }

        [HttpGet]
        public IActionResult AuthenticationForm()
        {
            return View("authentication_form");
        }
        [HttpPost]
        public async Task<ActionResult> Authentication(Auth_Client client)
        {
            var form = HttpContext.Request.Form;

            if (client == null)
            {
                return Redirect("~/Client/AuthenticationError");
            }

            if (client.Email == null || client.Password == null)
            {
                return Redirect("~/Client/AuthenticationError");
            }

            var email = client.Email;
            var password = client.Password;

            Client current_client = database.Clients.FirstOrDefault(client => client.ClientEmail == email && client.ClientPassword == password);

            if (current_client == null)
            {
                return Redirect("~/Client/AuthenticationError");
            }
            

            var claimsList = new List<Claim>();

            claimsList.Add(new Claim(ClaimTypes.Email, current_client.ClientEmail));
            claimsList.Add(new Claim(ClaimTypes.Name, current_client.ClientName));
            claimsList.Add(new Claim(ClaimTypes.Surname, current_client.ClientSurname));

            var claimsIdentity = new ClaimsIdentity(claimsList, "Cookies");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));



            return Redirect("~/Client/Index");

        }

        public IActionResult Profile()
        {
            if (User.FindFirst(ClaimTypes.Email) == null)
            {
                return Redirect("~/Client/Index");
            }
            else
            {
                Client client = new Client
                {
                    ClientEmail = User.FindFirst(ClaimTypes.Email).Value,
                    ClientName = User.FindFirst(ClaimTypes.Name).Value,
                    ClientSurname = User.FindFirst(ClaimTypes.Surname).Value,
                };

                return View(client);
            }
            
        }



        public IActionResult AuthenticationError()
        {
            return Content("Form dont containstd gmail or password");
        }

        public IActionResult AccessDenied()
        {
            return Content("This user does not exist. Or email or password incorrect");
        }




        // в панель разработчика, можно такое добавить

        public IActionResult DeleteAllClients()
        {
            using (database)
            {
                database.Database.OpenConnection();
                var client_to_delete = from client in database.Clients
                                       where client.ClientId > 0 || client.ClientId < 100
                                       select client;
                try
                {
                    database.Clients.RemoveRange(client_to_delete);
                }
                catch (Exception ex)
                {
                    Content(ex.ToString());
                }
                database.SaveChanges();
                database.Database.CloseConnection();
            }
            return Content("All users successfully delete!");

        }

        public IActionResult GetAllClients()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var client in database.Clients)
            {
                sb.Append(client.ClientEmail);
                sb.Append("\n");
            }
            return Content(sb.ToString());
        }

        public IActionResult UpdateMemoryCache()
        {
            MemoryCache.Set();
            var clients = MemoryCache.Get();
            return Content(clients);
        }

        public async Task<ActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return Redirect("~/Client/Index");
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult Email(string email)
        {
            return Json(false);
        }

        public IActionResult getrequestpath()
        {
            string path = HttpContext.Request.Path;
            return Content(path);
        }

        public string GenerateUserId()
        {
            string symbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            Random rand = new Random();
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < 15; i++)
            {
                int index = rand.Next(0, symbols.Length - 1);
                sb.Append(symbols[index]);
            }
            string userid = sb.ToString();
            return userid;
            
            // claimTypes.UserIdentifier("id", "userid)
        }


    }
}
