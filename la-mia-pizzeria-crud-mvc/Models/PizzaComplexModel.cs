namespace la_mia_pizzeria_crud.Models
{
    public class PizzaComplexModel
    {
        public Pizza Pizza { get; set; }
        public List<Category>? Categories { get; set; }
    }
}
