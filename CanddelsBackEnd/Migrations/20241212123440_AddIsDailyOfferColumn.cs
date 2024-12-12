using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CanddelsBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDailyOfferColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDailyOffer",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDailyOffer",
                table: "Products");
        }
    }
}
