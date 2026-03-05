using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace nutridet_ai_api.Migrations
{
    /// <inheritdoc />
    public partial class CreateOutputNutritionVisual : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NutritionVisualRules",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "NutritionVisualRules",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "NutritionVisualRules",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "NutritionVisualRules",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "NutritionVisualRules",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "NutritionVisualRules",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "NutritionVisualRules",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "NutritionVisualRules",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.CreateTable(
                name: "OutputNutritionVisuals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OutputNutritionId = table.Column<int>(type: "integer", nullable: false),
                    Nutrient = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    OriginalValue = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    VisualAmount = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    VisualName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutputNutritionVisuals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OutputNutritionVisuals_OutputNutritions_OutputNutritionId",
                        column: x => x.OutputNutritionId,
                        principalTable: "OutputNutritions",
                        principalColumn: "OutputNutritionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OutputNutritionVisuals_OutputNutritionId",
                table: "OutputNutritionVisuals",
                column: "OutputNutritionId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutputNutritionVisuals");

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
    }
}
