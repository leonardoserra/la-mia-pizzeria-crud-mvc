using Microsoft.AspNetCore.Mvc;
using la_mia_pizzeria_crud.Models;
using la_mia_pizzeria_crud.Database;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {
            using(PizzeriaContext db = new PizzeriaContext())
            {
                List<Pizza> pizzas = db.Pizzas.ToList<Pizza>();
                return View("Index",pizzas);

            }
        }

        public IActionResult Details(int id)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza pizza = db.Pizzas.Where<Pizza>(p=>p.Id==id).First();
                return View("Details", pizza);

            }
        }

        public IActionResult UserIndex()
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                List<Pizza> pizzas = db.Pizzas.ToList<Pizza>();
                return View("Index", pizzas);

            }
        }
    }
}
