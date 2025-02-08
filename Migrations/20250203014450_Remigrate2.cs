using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoShopRentalRevision.Migrations
{
    /// <inheritdoc />
    public partial class Remigrate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "RentalDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "RentalDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
