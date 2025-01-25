using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CanddelsBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomProductScent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Scent1",
                table: "customProducts");

            migrationBuilder.DropColumn(
                name: "Scent2",
                table: "customProducts");

            migrationBuilder.DropColumn(
                name: "Scent3",
                table: "customProducts");

            migrationBuilder.DropColumn(
                name: "Scent4",
                table: "customProducts");

            migrationBuilder.CreateTable(
                name: "CustomProductScent",
                columns: table => new
                {
                    CustomProductId = table.Column<int>(type: "int", nullable: false),
                    ScentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomProductScent", x => new { x.CustomProductId, x.ScentId });
                    table.ForeignKey(
                        name: "FK_CustomProductScent_Scents_ScentId",
                        column: x => x.ScentId,
                        principalTable: "Scents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomProductScent_customProducts_CustomProductId",
                        column: x => x.CustomProductId,
                        principalTable: "customProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomProductScent_ScentId",
                table: "CustomProductScent",
                column: "ScentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomProductScent");

            migrationBuilder.AddColumn<string>(
                name: "Scent1",
                table: "customProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Scent2",
                table: "customProducts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Scent3",
                table: "customProducts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Scent4",
                table: "customProducts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
