using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_crud.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage ="Richiesto il nome della categoria")]
        [StringLength(50, ErrorMessage ="Massimo 50 caratteri")]
        public string Name { get; set; }

        List<Pizza>? Pizzas { get; set; }

        public Category() { }   
    }
}
