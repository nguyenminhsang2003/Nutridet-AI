using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace nutridet_ai_api.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ScanImages",
                columns: table => new
                {
                    ScanImageId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    AiProvider = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    RawTextResponse = table.Column<string>(type: "text", nullable: true),
                    AiConfidence = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    ConfidenceStatus = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScanImages", x => x.ScanImageId);
                    table.ForeignKey(
                        name: "FK_ScanImages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OutputNutritions",
                columns: table => new
                {
                    AiRawId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ScanImageId = table.Column<int>(type: "integer", nullable: false),
                    EnergyKcal = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    CarbohydrateG = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    SugarG = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    ProteinG = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    FatG = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    SaturatedFatG = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    FiberG = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    SodiumMg = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    CholesterolMg = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutputNutritions", x => x.AiRawId);
                    table.ForeignKey(
                        name: "FK_OutputNutritions_ScanImages_ScanImageId",
                        column: x => x.ScanImageId,
                        principalTable: "ScanImages",
                        principalColumn: "ScanImageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OutputNutritions_ScanImageId",
                table: "OutputNutritions",
                column: "ScanImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScanImages_UserId",
                table: "ScanImages",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutputNutritions");

            migrationBuilder.DropTable(
                name: "ScanImages");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
