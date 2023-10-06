using la_mia_pizzeria_crud.Database;
using la_mia_pizzeria_crud.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace la_mia_pizzeria_crud.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserIndex()
        {
            if (User.IsInRole("ADMIN"))
            {
                return Redirect("/Pizza/Index");
            }
                List<Pizza> pizzas = new List<Pizza>();
            try
            {
                using (PizzeriaContext db = new PizzeriaContext())
                {
                    pizzas = db.Pizzas.ToList<Pizza>();
                    return View("UserIndex", pizzas);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}