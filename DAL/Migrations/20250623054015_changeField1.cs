using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class changeField1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_Appointment_Rating_rating_id",
                table: "Service_Appointment");

            migrationBuilder.AlterColumn<Guid>(
                name: "rating_id",
                table: "Service_Appointment",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Appointment_Rating_rating_id",
                table: "Service_Appointment",
                column: "rating_id",
                principalTable: "Rating",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_Appointment_Rating_rating_id",
                table: "Service_Appointment");

            migrationBuilder.AlterColumn<Guid>(
                name: "rating_id",
                table: "Service_Appointment",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Appointment_Rating_rating_id",
                table: "Service_Appointment",
                column: "rating_id",
                principalTable: "Rating",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
