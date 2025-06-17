using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class newdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Service_Appointment",
                table: "Service_Appointment");

            migrationBuilder.RenameColumn(
                name: "end_type",
                table: "Service_Appointment",
                newName: "end_time");

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "Service_Appointment",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Service_Appointment",
                table: "Service_Appointment",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_Service_Appointment_service_id",
                table: "Service_Appointment",
                column: "service_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Service_Appointment",
                table: "Service_Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Service_Appointment_service_id",
                table: "Service_Appointment");

            migrationBuilder.DropColumn(
                name: "id",
                table: "Service_Appointment");

            migrationBuilder.RenameColumn(
                name: "end_time",
                table: "Service_Appointment",
                newName: "end_type");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Service_Appointment",
                table: "Service_Appointment",
                column: "service_id");
        }
    }
}
