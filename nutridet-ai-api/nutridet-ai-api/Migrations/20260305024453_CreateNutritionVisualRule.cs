using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace nutridet_ai_api.Migrations
{
    /// <inheritdoc />
    public partial class CreateNutritionVisualRule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NutritionVisualRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nutrient = table.Column<string>(type: "text", nullable: false),
                    ReferenceAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    VisualName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutritionVisualRules", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "NutritionVisualRules",
                columns: new[] { "Id", "Nutrient", "ReferenceAmount", "VisualName" },
                values: new object[,]
                {
                    { 1, "carbohydrateG", 15m, "bread slice" },
                    { 2, "sugarG", 4m, "sugar cube" },
                    { 3, "proteinG", 6m, "egg" },
                    { 4, "fatG", 14m, "oil spoon" },
                    { 5, "saturatedFatG", 5m, "butter" },
                    { 6, "fiberG", 4m, "apple" },
                    { 7, "sodiumMg", 400m, "salt gram" },
                    { 8, "cholesterolMg", 186m, "egg cholesterol" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NutritionVisualRules");
        }
    }
}
