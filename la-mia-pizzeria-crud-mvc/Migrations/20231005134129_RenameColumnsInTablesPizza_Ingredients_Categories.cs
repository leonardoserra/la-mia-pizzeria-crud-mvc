using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace la_mia_pizzeria_crud.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumnsInTablesPizza_Ingredients_Categories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pizzas_categories_CategoryId",
                table: "pizzas");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "pizzas",
                newName: "category_id");

            migrationBuilder.RenameIndex(
                name: "IX_pizzas_CategoryId",
                table: "pizzas",
                newName: "IX_pizzas_category_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ingredients",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ingredients",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "categories",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "categories",
                newName: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_pizzas_categories_category_id",
                table: "pizzas",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pizzas_categories_category_id",
                table: "pizzas");

            migrationBuilder.RenameColumn(
                name: "category_id",
                table: "pizzas",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_pizzas_category_id",
                table: "pizzas",
                newName: "IX_pizzas_CategoryId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "ingredients",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ingredients",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "categories",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "categories",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_pizzas_categories_CategoryId",
                table: "pizzas",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id");
        }
    }
}
