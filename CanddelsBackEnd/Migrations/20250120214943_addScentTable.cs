using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CanddelsBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class addScentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Scent",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ScentId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Scents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scents", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ScentId",
                table: "Products",
                column: "ScentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Scents_ScentId",
                table: "Products",
                column: "ScentId",
                principalTable: "Scents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Scents_ScentId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Scents");

            migrationBuilder.DropIndex(
                name: "IX_Products_ScentId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ScentId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Scent",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
