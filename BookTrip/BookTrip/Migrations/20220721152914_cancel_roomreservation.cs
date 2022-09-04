using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookTrip.Migrations
{
    public partial class cancel_roomreservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeletionToken",
                table: "RoomReservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "RoomReservations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletionToken",
                table: "RoomReservations");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "RoomReservations");
        }
    }
}
