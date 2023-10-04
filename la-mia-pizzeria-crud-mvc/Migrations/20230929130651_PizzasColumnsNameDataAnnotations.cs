using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace la_mia_pizzeria_crud.Migrations
{
    /// <inheritdoc />
    public partial class PizzasColumnsNameDataAnnotations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Pizzas",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Pizzas",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Pizzas",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Pizzas",
                newName: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "price",
                table: "Pizzas",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Pizzas",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Pizzas",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Pizzas",
                newName: "Id");
        }
    }
}
