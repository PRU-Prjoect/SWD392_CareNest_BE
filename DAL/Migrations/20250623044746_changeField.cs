using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class changeField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_Appointment_Room_room_id",
                table: "Service_Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Service_Appointment_room_id",
                table: "Service_Appointment");

            migrationBuilder.DropColumn(
                name: "end_time",
                table: "Service_Appointment");

            migrationBuilder.DropColumn(
                name: "room_id",
                table: "Service_Appointment");

            migrationBuilder.DropColumn(
                name: "start_time",
                table: "Service_Appointment");

            migrationBuilder.DropColumn(
                name: "end_time",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "location_type",
                table: "Appointments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "end_time",
                table: "Service_Appointment",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "room_id",
                table: "Service_Appointment",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "start_time",
                table: "Service_Appointment",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "end_time",
                table: "Appointments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "location_type",
                table: "Appointments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Service_Appointment_room_id",
                table: "Service_Appointment",
                column: "room_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Appointment_Room_room_id",
                table: "Service_Appointment",
                column: "room_id",
                principalTable: "Room",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
