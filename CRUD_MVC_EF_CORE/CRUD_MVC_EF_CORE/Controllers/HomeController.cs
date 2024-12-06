using CRUD_MVC_EF_CORE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CRUD_MVC_EF_CORE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly EFCoreDBContext _context;

        public HomeController(ILogger<HomeController> logger, EFCoreDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var eFCoreDbContext = _context.Products;
            return View(eFCoreDbContext);
        }

        public IActionResult Create()
        {            
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Name,Code, Status")] Product pr)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    using var client = new HttpClient();

                    string json = JsonConvert.SerializeObject(pr);
                    var requs = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5079/Product");
                    var content = new StringContent(json, null, "application/json");
                    requs.Content = content;

                    var response = await client.SendAsync(requs);
                    // Si el servicio responde correctamente
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var lr = JsonConvert.DeserializeObject<Product>(result);
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return View(pr);

        }


        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
            var employee = await _context.Products.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
            var employee = await _context.Products.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }            
            return View(employee);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name,Code, Status")] Product pr)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using var client = new HttpClient();

                    string json = JsonConvert.SerializeObject(pr);
                    var requs = new HttpRequestMessage(HttpMethod.Put, "http://localhost:5079/Product/" + id);
                    var content = new StringContent(json, null, "application/json");
                    requs.Content = content;

                    var response = await client.SendAsync(requs);
                    // Si el servicio responde correctamente
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var lr = JsonConvert.DeserializeObject<Product>(result);
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return View(pr);

           
        }


        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
            var pr = await _context.Products.FindAsync(id);
            if (pr == null)
            {
                return NotFound();
            }
            return View(pr);
        }
               

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {

            try
            {
                using var client = new HttpClient();

                var requs = new HttpRequestMessage(HttpMethod.Delete, "http://localhost:5079/Product/" + id);
                var content = new StringContent("application/json");
                requs.Content = content;

                var response = await client.SendAsync(requs);
                // Si el servicio responde correctamente
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var lr = JsonConvert.DeserializeObject<Product>(result);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
                       
        }

        private bool ProductExists(long id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
