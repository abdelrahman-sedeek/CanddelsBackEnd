using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CanddelsBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class addMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomProductScent_Scents_ScentId",
                table: "CustomProductScent");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomProductScent_customProducts_CustomProductId",
                table: "CustomProductScent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomProductScent",
                table: "CustomProductScent");

            migrationBuilder.RenameTable(
                name: "CustomProductScent",
                newName: "CustomProductScents");

            migrationBuilder.RenameIndex(
                name: "IX_CustomProductScent_ScentId",
                table: "CustomProductScents",
                newName: "IX_CustomProductScents_ScentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomProductScents",
                table: "CustomProductScents",
                columns: new[] { "CustomProductId", "ScentId" });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CustomProductScents_Scents_ScentId",
                table: "CustomProductScents",
                column: "ScentId",
                principalTable: "Scents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomProductScents_customProducts_CustomProductId",
                table: "CustomProductScents",
                column: "CustomProductId",
                principalTable: "customProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomProductScents_Scents_ScentId",
                table: "CustomProductScents");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomProductScents_customProducts_CustomProductId",
                table: "CustomProductScents");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomProductScents",
                table: "CustomProductScents");

            migrationBuilder.RenameTable(
                name: "CustomProductScents",
                newName: "CustomProductScent");

            migrationBuilder.RenameIndex(
                name: "IX_CustomProductScents_ScentId",
                table: "CustomProductScent",
                newName: "IX_CustomProductScent_ScentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomProductScent",
                table: "CustomProductScent",
                columns: new[] { "CustomProductId", "ScentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CustomProductScent_Scents_ScentId",
                table: "CustomProductScent",
                column: "ScentId",
                principalTable: "Scents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomProductScent_customProducts_CustomProductId",
                table: "CustomProductScent",
                column: "CustomProductId",
                principalTable: "customProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
