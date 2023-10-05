using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_crud.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Il nome dell'ingrediente é obbligatorio")]
        [StringLength(30, ErrorMessage ="Massimo 30 caratteri")]
        public string Name { get; set; }

        public List<Pizza>? Pizzas { get; set; }
    }
}
