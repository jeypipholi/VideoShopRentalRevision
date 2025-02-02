using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoShopRentalRevision.Migrations
{
    /// <inheritdoc />
    public partial class updateTables1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieRental",
                columns: table => new
                {
                    MoviesMovieId = table.Column<int>(type: "int", nullable: false),
                    RentalsRentalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieRental", x => new { x.MoviesMovieId, x.RentalsRentalId });
                    table.ForeignKey(
                        name: "FK_MovieRental_Movies_MoviesMovieId",
                        column: x => x.MoviesMovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieRental_Rentals_RentalsRentalId",
                        column: x => x.RentalsRentalId,
                        principalTable: "Rentals",
                        principalColumn: "RentalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieRental_RentalsRentalId",
                table: "MovieRental",
                column: "RentalsRentalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieRental");
        }
    }
}
