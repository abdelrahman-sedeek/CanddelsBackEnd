using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CanddelsBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_ProductVariants_ProductVariantId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ProductVariants_productVariantId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_productVariantId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ProductVariantId",
                table: "CartItems");

            migrationBuilder.AlterColumn<int>(
                name: "productVariantId",
                table: "OrderItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "customProductId",
                table: "OrderItems",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductVariantId",
                table: "CartItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CustomProductId",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "customProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Scent1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Scent2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Scent3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Scent4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customProducts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_customProductId",
                table: "OrderItems",
                column: "customProductId",
                unique: true,
                filter: "[customProductId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_productVariantId",
                table: "OrderItems",
                column: "productVariantId",
                unique: true,
                filter: "[productVariantId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CustomProductId",
                table: "CartItems",
                column: "CustomProductId",
                unique: true,
                filter: "[CustomProductId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductVariantId",
                table: "CartItems",
                column: "ProductVariantId",
                unique: true,
                filter: "[ProductVariantId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_ProductVariants_ProductVariantId",
                table: "CartItems",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_customProducts_CustomProductId",
                table: "CartItems",
                column: "CustomProductId",
                principalTable: "customProducts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ProductVariants_productVariantId",
                table: "OrderItems",
                column: "productVariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_customProducts_customProductId",
                table: "OrderItems",
                column: "customProductId",
                principalTable: "customProducts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_ProductVariants_ProductVariantId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_customProducts_CustomProductId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ProductVariants_productVariantId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_customProducts_customProductId",
                table: "OrderItems");

            migrationBuilder.DropTable(
                name: "customProducts");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_customProductId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_productVariantId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_CustomProductId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ProductVariantId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "customProductId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "CustomProductId",
                table: "CartItems");

            migrationBuilder.AlterColumn<int>(
                name: "productVariantId",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductVariantId",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_productVariantId",
                table: "OrderItems",
                column: "productVariantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductVariantId",
                table: "CartItems",
                column: "ProductVariantId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_ProductVariants_ProductVariantId",
                table: "CartItems",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ProductVariants_productVariantId",
                table: "OrderItems",
                column: "productVariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
