using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DulcesPro.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIngredientPriceAndUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PricePerKg",
                table: "Ingredients",
                newName: "Price");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Ingredients");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Ingredients",
                newName: "PricePerKg");
        }
    }
}
