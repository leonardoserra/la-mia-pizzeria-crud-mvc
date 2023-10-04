using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_crud.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        List<Pizza>? Pizzas { get; set; }

        public Category() { }   
    }
}
