using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookTrip.Migrations
{
    public partial class updateTaxiReservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "TaxiReservations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "TaxiReservations");
        }
    }
}
