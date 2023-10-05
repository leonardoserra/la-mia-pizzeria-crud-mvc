using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_crud.Models
{
    [Table("categories")]
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
