using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CanddelsBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class SmallUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_OrderItems_OrderItemId",
                table: "ProductVariants");

            migrationBuilder.DropIndex(
                name: "IX_ProductVariants_OrderItemId",
                table: "ProductVariants");

            migrationBuilder.RenameColumn(
                name: "Benefits",
                table: "Products",
                newName: "Benfits");

            migrationBuilder.AlterColumn<int>(
                name: "OrderItemId",
                table: "ProductVariants",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CartItemId",
                table: "ProductVariants",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_OrderItemId",
                table: "ProductVariants",
                column: "OrderItemId",
                unique: true,
                filter: "[OrderItemId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_OrderItems_OrderItemId",
                table: "ProductVariants",
                column: "OrderItemId",
                principalTable: "OrderItems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_OrderItems_OrderItemId",
                table: "ProductVariants");

            migrationBuilder.DropIndex(
                name: "IX_ProductVariants_OrderItemId",
                table: "ProductVariants");

            migrationBuilder.RenameColumn(
                name: "Benfits",
                table: "Products",
                newName: "Benefits");

            migrationBuilder.AlterColumn<int>(
                name: "OrderItemId",
                table: "ProductVariants",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CartItemId",
                table: "ProductVariants",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_OrderItemId",
                table: "ProductVariants",
                column: "OrderItemId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_OrderItems_OrderItemId",
                table: "ProductVariants",
                column: "OrderItemId",
                principalTable: "OrderItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
