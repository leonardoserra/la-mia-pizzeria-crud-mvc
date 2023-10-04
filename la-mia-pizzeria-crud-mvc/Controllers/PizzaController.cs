using Microsoft.AspNetCore.Mvc;
using la_mia_pizzeria_crud.Models;
using la_mia_pizzeria_crud.Database;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using la_mia_pizzeria_crud.CustomLoggers;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_crud.Controllers
{
    public class PizzaController : Controller
    {   
        private PizzeriaContext _db;
        private ICustomLogger _logger;
        public PizzaController(PizzeriaContext db, ICustomLogger logger)
        {
            //created with dependence injections
            _db = db;
            _logger = logger;
        }
        public IActionResult Index()
        {
            _logger.WriteLog($"utente entrato in index");
            List<Pizza> pizzas = new List<Pizza>();
            try
            {
                pizzas = _db.Pizzas.Include(pizza => pizza.Category).ToList<Pizza>();
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
            _logger.WriteLog($"utente entrato in dettaglio item {id}");

            try
            {
              
                Pizza pizza = _db.Pizzas.Include(pizza=>pizza.Category).Where<Pizza>(p=>p.Id==id).First();
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



        //CRUD
        [HttpGet]
        public IActionResult Create()
        {
            _logger.WriteLog($"utente entrato in creazione");

            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza newPizza)
        {
            _logger.WriteLog($"utente prova a creare {newPizza.Name}");

            if (!ModelState.IsValid)
            {
                return View("Create", newPizza);
            }
            
           
            //default image se null
            if (newPizza.ImagePath == null)
            {
                newPizza.ImagePath = "/img/default.png";
            }
            _db.Pizzas.Add(newPizza);
            _db.SaveChanges();
            _logger.WriteLog($"utente ha creato {newPizza.Name}");

            return RedirectToAction("Index");
            
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            _logger.WriteLog($"utente entrato in modifica elemento {id}");

            Pizza? pizzaToUpdate = _db.Pizzas.Where(pizza=>pizza.Id == id).FirstOrDefault();
            if(pizzaToUpdate == null)
                return View("Error");

            return View("Update", pizzaToUpdate);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Pizza pizzaReceived)
        {
            _logger.WriteLog($"utente prova a salvare {pizzaReceived.Name} con id {id}");

            if (pizzaReceived.ImagePath == null)
            {
                pizzaReceived.ImagePath = "/img/default.png";
            }
            if (!ModelState.IsValid)
                return View("Update", pizzaReceived);
                
            Pizza? pizzaToUpdate = _db.Pizzas.Find(id);
            if (pizzaToUpdate == null)
                return View("Error");

            EntityEntry<Pizza> updatedPizza = _db.Entry(pizzaToUpdate);
            updatedPizza.CurrentValues.SetValues(pizzaReceived);
            _db.SaveChanges();
            _logger.WriteLog($"utente salva {pizzaReceived.Name} con id {id}");


            return RedirectToAction("Index");
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {

           
            Pizza? pizzaToDelete = _db.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();
            if (pizzaToDelete == null)
                return View("Error");
               
               
            _db.Pizzas.Remove(pizzaToDelete);
            _db.SaveChanges();
            _logger.WriteLog($"utente ha eliminato {pizzaToDelete.Name} con id {id}");

            return RedirectToAction("Index");
            
        }

    }
}
