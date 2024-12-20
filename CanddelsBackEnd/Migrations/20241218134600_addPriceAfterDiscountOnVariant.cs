using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CanddelsBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class addPriceAfterDiscountOnVariant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceAfterDiscount",
                table: "Discounts");

            migrationBuilder.AddColumn<decimal>(
                name: "PriceAfterDiscount",
                table: "ProductVariants",
                type: "decimal(5,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceAfterDiscount",
                table: "ProductVariants");

            migrationBuilder.AddColumn<decimal>(
                name: "PriceAfterDiscount",
                table: "Discounts",
                type: "decimal(5,2)",
                nullable: true);
        }
    }
}
