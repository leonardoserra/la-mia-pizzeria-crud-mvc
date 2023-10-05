using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_crud.Models
{
    [Table("ingredients")]

    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Il nome dell'ingrediente é obbligatorio")]
        [StringLength(30, ErrorMessage ="Massimo 30 caratteri")]
        public string Name { get; set; }

        public List<Pizza>? Pizzas { get; set; }

        public Ingredient() { }
    }
}
