using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaTicketBookingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexToCreatedDateUtc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CreatedDateUtc",
                table: "Reservations",
                column: "CreatedDateUtc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reservations_CreatedDateUtc",
                table: "Reservations");
        }
    }
}
