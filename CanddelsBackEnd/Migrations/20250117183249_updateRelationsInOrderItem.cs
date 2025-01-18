using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CanddelsBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class updateRelationsInOrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ProductVariants_productVariantId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_customProducts_customProductId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_customProductId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_productVariantId",
                table: "OrderItems");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_customProductId",
                table: "OrderItems",
                column: "customProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_productVariantId",
                table: "OrderItems",
                column: "productVariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ProductVariants_productVariantId",
                table: "OrderItems",
                column: "productVariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_customProducts_customProductId",
                table: "OrderItems",
                column: "customProductId",
                principalTable: "customProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ProductVariants_productVariantId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_customProducts_customProductId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_customProductId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_productVariantId",
                table: "OrderItems");

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
    }
}
