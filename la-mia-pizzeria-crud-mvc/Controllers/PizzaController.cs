using Microsoft.AspNetCore.Mvc;
using la_mia_pizzeria_crud.Models;
using la_mia_pizzeria_crud.Database;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {
            List<Pizza> pizzas = new List<Pizza>();
            try
            {
                using(PizzeriaContext db = new PizzeriaContext())
                {
                    pizzas = db.Pizzas.ToList<Pizza>();
                    return View("Index",pizzas);

                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
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
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
                             
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza newPizza)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", newPizza);
            }
            
            using(PizzeriaContext db = new PizzeriaContext())
            {

                //default image se null
                if (newPizza.ImagePath == null)
                {
                    newPizza.ImagePath = "/img/default.png";
                }
                db.Pizzas.Add(newPizza);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            using(PizzeriaContext db = new PizzeriaContext())
            {
                Pizza? pizzaToUpdate = db.Pizzas.Where(pizza=>pizza.Id == id).FirstOrDefault();
                if(pizzaToUpdate == null)
                    return View("Error");

                return View("Update", pizzaToUpdate);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Pizza pizzaReceived)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {

                if (!ModelState.IsValid)
                    return View("Update", pizzaReceived);
                
                Pizza? pizzaToUpdate = db.Pizzas.Find(id);
                if (pizzaToUpdate == null)
                    return View("Error");

                EntityEntry<Pizza> updatedPizza = db.Entry(pizzaToUpdate);
                updatedPizza.CurrentValues.SetValues(pizzaReceived);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {

            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza? pizzaToDelete = db.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();
                if (pizzaToDelete == null)
                    return View("Error");
               
               
                db.Pizzas.Remove(pizzaToDelete);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

    }
}
