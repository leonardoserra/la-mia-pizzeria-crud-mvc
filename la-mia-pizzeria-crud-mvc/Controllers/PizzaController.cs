using Microsoft.AspNetCore.Mvc;
using la_mia_pizzeria_crud.Models;
using la_mia_pizzeria_crud.Database;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                using(PizzeriaContext db = new PizzeriaContext())
                {
                    List<Pizza> pizzas = db.Pizzas.ToList<Pizza>();
                    return View("Index",pizzas);

                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View("Error");
        }

        public IActionResult Details(int id)
        {
            try
            {
                using (PizzeriaContext db = new PizzeriaContext())
                {
                    Pizza pizza = db.Pizzas.Where<Pizza>(p=>p.Id==id).First();
                    return View("Details", pizza);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View("Error");
        }

        public IActionResult UserIndex()
        {
            try
            { 
                using (PizzeriaContext db = new PizzeriaContext())
                {
                    List<Pizza> pizzas = db.Pizzas.ToList<Pizza>();
                    return View("UserIndex", pizzas);

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View("Error");
        }
    }
}
