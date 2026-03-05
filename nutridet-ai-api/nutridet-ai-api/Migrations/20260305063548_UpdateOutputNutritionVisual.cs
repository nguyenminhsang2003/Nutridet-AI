using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nutridet_ai_api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOutputNutritionVisual : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OutputNutritionVisuals_OutputNutritionId",
                table: "OutputNutritionVisuals");

            migrationBuilder.CreateIndex(
                name: "IX_OutputNutritionVisuals_OutputNutritionId",
                table: "OutputNutritionVisuals",
                column: "OutputNutritionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OutputNutritionVisuals_OutputNutritionId",
                table: "OutputNutritionVisuals");

            migrationBuilder.CreateIndex(
                name: "IX_OutputNutritionVisuals_OutputNutritionId",
                table: "OutputNutritionVisuals",
                column: "OutputNutritionId",
                unique: true);
        }
    }
}
