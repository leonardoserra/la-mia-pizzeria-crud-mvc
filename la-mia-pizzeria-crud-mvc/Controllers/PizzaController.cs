using Microsoft.AspNetCore.Mvc;
using la_mia_pizzeria_crud.Models;
using la_mia_pizzeria_crud.Database;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {   
        private PizzeriaContext _myDB;
        public PizzaController(PizzeriaContext db) {
            _myDB = db;
        }
        public IActionResult Index()
        {
            List<Pizza> pizzas = new List<Pizza>();
            try
            {
               
                pizzas = _myDB.Pizzas.ToList<Pizza>();
                return View("Index",pizzas);

             

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
              
                Pizza pizza = _myDB.Pizzas.Where<Pizza>(p=>p.Id==id).First();
                return View("Details", pizza);
                
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
            
           
            //default image se null
            if (newPizza.ImagePath == null)
            {
                newPizza.ImagePath = "/img/default.png";
            }
            _myDB.Pizzas.Add(newPizza);
            _myDB.SaveChanges();
            return RedirectToAction("Index");
            
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
           
            Pizza? pizzaToUpdate = _myDB.Pizzas.Where(pizza=>pizza.Id == id).FirstOrDefault();
            if(pizzaToUpdate == null)
                return View("Error");

            return View("Update", pizzaToUpdate);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Pizza pizzaReceived)
        {
         
            if (!ModelState.IsValid)
                return View("Update", pizzaReceived);
                
            Pizza? pizzaToUpdate = _myDB.Pizzas.Find(id);
            if (pizzaToUpdate == null)
                return View("Error");

            EntityEntry<Pizza> updatedPizza = _myDB.Entry(pizzaToUpdate);
            updatedPizza.CurrentValues.SetValues(pizzaReceived);
            _myDB.SaveChanges();

            return RedirectToAction("Index");
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {

           
            Pizza? pizzaToDelete = _myDB.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();
            if (pizzaToDelete == null)
                return View("Error");
               
               
            _myDB.Pizzas.Remove(pizzaToDelete);
            _myDB.SaveChanges();
            return RedirectToAction("Index");
            
        }

    }
}
