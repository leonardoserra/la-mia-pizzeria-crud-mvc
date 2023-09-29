using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace la_mia_pizzeria_crud.Models
{
    public class Pizza 
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        
        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        [DefaultValue("")]
        public string? Description { get; set; }

        [Column("price")]
        public float? Price { get; set; }

    }
}
