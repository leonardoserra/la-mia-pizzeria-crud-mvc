using la_mia_pizzeria_crud.Models;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_crud.Database
{
    public class PizzeriaContext :DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=db-pizzeria;Integrated Security=True;TrustServerCertificate=True");
        }

    }
}
