using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nutridet_ai_api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOutputNutrition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AiRawId",
                table: "OutputNutritions",
                newName: "OutputNutritionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OutputNutritionId",
                table: "OutputNutritions",
                newName: "AiRawId");
        }
    }
}
