using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nutridet_ai_api.Migrations
{
    /// <inheritdoc />
    public partial class updateExcersice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDone",
                table: "OutputNutritionExcercises",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDone",
                table: "OutputNutritionExcercises");
        }
    }
}
