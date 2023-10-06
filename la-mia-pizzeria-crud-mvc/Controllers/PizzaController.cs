using Microsoft.AspNetCore.Mvc;
using la_mia_pizzeria_crud.Models;
using la_mia_pizzeria_crud.Database;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using la_mia_pizzeria_crud.CustomLoggers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace la_mia_pizzeria_crud.Controllers
{
    [Authorize(Roles ="ADMIN,USER")]
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


        [Authorize(Roles = "ADMIN")]
        public IActionResult Index()
        {
            _logger.WriteLog($"utente entrato in index");
            List<Pizza> pizzas = new List<Pizza>();
            try
            {
                pizzas = _db.Pizzas.Include(pizza => pizza.Category).Include(pizza => pizza.Ingredients).ToList<Pizza>();
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
                Pizza pizza = _db.Pizzas.Include(pizza=>pizza.Category).Include(pizza => pizza.Ingredients).Where<Pizza>(p=>p.Id==id).First();
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
        [Authorize(Roles = "ADMIN")]

        public IActionResult Create()
        {
            _logger.WriteLog($"utente entrato in creazione");
            List<Category> categories = _db.Categories.ToList();
            List<Ingredient> allIngredients= _db.Ingredients.ToList();

            List<SelectListItem> ingredientsToSend = new();
            foreach(Ingredient ingredient in allIngredients)
            {
                ingredientsToSend.Add(new SelectListItem { Text = ingredient.Name, Value = ingredient.Id.ToString() });
            }
            PizzaComplexModel dataToSend = new PizzaComplexModel {
                Pizza = new Pizza {ImagePath = "" },
                Categories = categories, 
                Ingredients = ingredientsToSend 
            };
            return View("Create", dataToSend);
        }

        

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaComplexModel receivedData)
        {
            _logger.WriteLog($"utente prova a creare {receivedData.Pizza.Name}");

            //se validazione fallisce reinvio i dati alla vista
            if (!ModelState.IsValid)
            {
                List<Category> categories = _db.Categories.ToList();
                List<Ingredient> allIngredients = _db.Ingredients.ToList();
                List<SelectListItem> ingredientsToSend = new();
                foreach (Ingredient ingredient in allIngredients)
                {
                    ingredientsToSend.Add(
                        new SelectListItem { 
                            Text = ingredient.Name,
                            Value = ingredient.Id.ToString(),
                        });
                }
                receivedData.Categories = categories;
                receivedData.Ingredients = ingredientsToSend;
                return View("Create", receivedData);
            }

            //se validazione passa scrivo in DB

            //default image se null o ""
            if (receivedData.Pizza.ImagePath == null)
            {
                receivedData.Pizza.ImagePath = "/img/default.png";
            }
            //aggiungiamo ingredienti alla pizza creata nel db
            receivedData.Pizza.Ingredients = new List<Ingredient>();
            if(receivedData.SelectedIngredientsId != null)
            {
                foreach(string ingredientsId in receivedData.SelectedIngredientsId)
                {
                    int selectedIngredientId = int.Parse(ingredientsId);
                    Ingredient? ingredientToAdd = _db.Ingredients.Where(ingredient => ingredient.Id == selectedIngredientId).FirstOrDefault();
                    receivedData.Pizza.Ingredients.Add(ingredientToAdd);
                }
            }
            _db.Pizzas.Add(receivedData.Pizza);
            _db.SaveChanges();
            _logger.WriteLog($"utente ha creato {receivedData.Pizza.Name}");

            return RedirectToAction("Index");
        }


        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Update(int id)
        {
            _logger.WriteLog($"utente entrato in modifica elemento {id}");

            Pizza? pizzaToUpdate = _db.Pizzas.Include(pizza => pizza.Category).Include(pizza=>pizza.Ingredients).Where(pizza=>pizza.Id == id).FirstOrDefault();
            
            if(pizzaToUpdate == null)
                return View("Error");

            //se img è default mostrami input vuoto
            if (pizzaToUpdate.ImagePath == "/img/default.png")
                pizzaToUpdate.ImagePath = "";
            List<Category> categories = _db.Categories.ToList();
            List<Ingredient> allIngredients = _db.Ingredients.ToList();
            List<SelectListItem> ingredientsToSend = new List<SelectListItem>();
            foreach(Ingredient ingredient in allIngredients)
            {
                ingredientsToSend.Add(new SelectListItem
                {
                    Text = ingredient.Name,
                    Value = ingredient.Id.ToString(),
                    Selected = pizzaToUpdate.Ingredients.Any(selectedIngredient => selectedIngredient.Id == ingredient.Id)
                });
            }

            PizzaComplexModel dataToSent = new PizzaComplexModel { Pizza = pizzaToUpdate, Categories = categories, Ingredients = ingredientsToSend };
            return View("Update", dataToSent);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzaComplexModel receivedData)
        {
            _logger.WriteLog($"utente prova a salvare {receivedData.Pizza.Name} con id {id}");

            if (!ModelState.IsValid)
            {
                List<Category> categories = _db.Categories.ToList();
                List<Ingredient> allIngredients = _db.Ingredients.ToList();
                List<SelectListItem> ingredientsToSend = new();
                foreach (Ingredient ingredient in allIngredients)
                {
                    ingredientsToSend.Add(
                        new SelectListItem
                        {
                            Text = ingredient.Name,
                            Value = ingredient.Id.ToString(),
                        });
                }
                receivedData.Categories = categories;
                receivedData.Ingredients = ingredientsToSend;
                return View("Update", receivedData);
            }

            //imposto ImagePath default se null o ""
            if (receivedData.Pizza.ImagePath == null || receivedData.Pizza.ImagePath == "")
                receivedData.Pizza.ImagePath = "/img/default.png";

            //scriviamo in DB
            Pizza? pizzaToUpdate = _db.Pizzas.Include(pizza=>pizza.Category).Include(pizza=>pizza.Ingredients).Where(pizza=>pizza.Id == id).FirstOrDefault();
            if (pizzaToUpdate == null)
                return View("Error");

            pizzaToUpdate.Ingredients = new List<Ingredient>();
            if (receivedData.SelectedIngredientsId != null)
            {
                foreach (string ingredientId in receivedData.SelectedIngredientsId) {
                    Ingredient? ingredientToAdd = _db.Ingredients.Where(ingredient => ingredient.Id == int.Parse(ingredientId)).FirstOrDefault();
                    if(ingredientToAdd != null) 
                    pizzaToUpdate.Ingredients.Add(ingredientToAdd); 
                }
            }
            EntityEntry<Pizza> updatedPizza = _db.Entry(pizzaToUpdate);
            updatedPizza.CurrentValues.SetValues(receivedData.Pizza);
            
            _db.SaveChanges();
            _logger.WriteLog($"utente salva {receivedData.Pizza.Name} con id {id}");

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
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
