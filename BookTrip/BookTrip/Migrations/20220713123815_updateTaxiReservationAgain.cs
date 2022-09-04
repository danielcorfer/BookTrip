using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookTrip.Migrations
{
    public partial class updateTaxiReservationAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeletionToken",
                table: "TaxiReservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletionToken",
                table: "TaxiReservations");
        }
    }
}
