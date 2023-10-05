using la_mia_pizzeria_crud.CustomValidations;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_crud.Models
{
    [Table("pizzas")]
    public class Pizza 
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        
        [Required(ErrorMessage ="Nome obbligatorio")]
        [StringLength(50,ErrorMessage ="Massimo 50 caratteri")]
        [Column("name")]
        public string Name { get; set; }

        [MoreThan5Words]
        [StringLength(500, ErrorMessage = "Massimo 500 caratteri")]
        [Column("description")]
        [DefaultValue("")]
        public string? Description { get; set; }

        [Required(ErrorMessage ="Inserire un prezzo iniziale della Pizza!")]
        [Column("price")]
        [Range(0.5,50,ErrorMessage ="Il prezzo di una pizza puo' variare da 0.5 a 50€")]
        public float? Price { get; set; } 

        [Column("image_path")]
        [DefaultValue("/img/default.png")]
        [Url(ErrorMessage = "Inserisci un url valido")]
        [StringLength(1000)]
        public string? ImagePath { get; set; } = "/img/default.png";

        [Column("category_id")]
        public int? CategoryId { get; set; }

        [Column("category")]
        public Category? Category { get; set; }

        [Column("ingredients")]
        public List<Ingredient>? Ingredients { get; set; }

        //constructors
        public Pizza() { }
        public Pizza(int id, string name, string? description, float? price, string? imagePath )
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            ImagePath = imagePath;
        }
    }
}
