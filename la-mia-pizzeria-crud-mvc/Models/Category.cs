using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_crud.Models
{
    [Table("categories")]
    public class Category
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        [Required(ErrorMessage ="Richiesto il nome della categoria")]
        [StringLength(50, ErrorMessage ="Massimo 50 caratteri")]
        public string Name { get; set; }
        [Column("pizzas")]
        List<Pizza>? Pizzas { get; set; }

        public Category() { }   
    }
}
