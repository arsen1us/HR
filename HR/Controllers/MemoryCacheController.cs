using HR_V2.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace HR_V2.Controllers
{
    public class MemoryCacheController : Controller
    {
        static IMemoryCache _memoryCache;
        static HR_Context _database;

        public MemoryCacheController( IMemoryCache memoryCache, HR_Context database)
        {
            _memoryCache = memoryCache;
            _database = database;
        }
        public async void Set<T>()
        {
            try
            {
                string json_clients = JsonSerializer.Serialize(_database.Clients);
                _memoryCache.Set("clients_db", json_clients);
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось загрузить данные в MemoryCache");
            }
            Console.WriteLine(true);
            Redirect("/memorycache/get");
        }

        public IActionResult Get()
        {
            string clients = string.Empty;
            if (!_memoryCache.TryGetValue("clients_db", out clients))
            {
                throw new Exception("Не удалось выгрузить данные из MemoryCache");
            }
            return Content(clients);

        }
        // Операция добавления;
        public IActionResult Add<T>(T table)
        {
            
            int indexType = 0;
            if(typeof(T) == typeof(List<Client>)) 
            {
               indexType = 1;
            }
            if (typeof(T) == typeof(List<Company>))
            {
                indexType = 2;
            }
            if (typeof(T) == typeof(List<WorkExperience>))
            {
                indexType = 3;
            }
            if (typeof(T) == typeof(List<Resume>))
            {
                indexType = 4;
            }
            if(table.GetType() == new List<Resume>().GetType())
            {
                string key = "resumes";
                _memoryCache.Set(key, JsonSerializer.Serialize<T>(table));
            }
            if (indexType == 0) throw new Exception("Undefined type");

            // or we can logg if operation has successfull complete
            return Redirect("~/Client/Index");
            
            
        }
    }
}
