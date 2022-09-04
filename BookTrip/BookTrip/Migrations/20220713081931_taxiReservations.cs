using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookTrip.Migrations
{
    public partial class taxiReservations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaxiReservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomReservationId = table.Column<int>(type: "int", nullable: false),
                    DepartureAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestedAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Departure = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxiReservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxiReservations_RoomReservations_RoomReservationId",
                        column: x => x.RoomReservationId,
                        principalTable: "RoomReservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaxiReservations_RoomReservationId",
                table: "TaxiReservations",
                column: "RoomReservationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaxiReservations");
        }
    }
}
