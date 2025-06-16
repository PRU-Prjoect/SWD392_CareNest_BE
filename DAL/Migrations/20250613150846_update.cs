using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Hotel_Hotelid",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Room_Hotelid",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "Hotelid",
                table: "Room");

            migrationBuilder.RenameColumn(
                name: "end_type",
                table: "Appointments",
                newName: "end_time");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "Room_Booking",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "Room_Booking",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "hotel_id",
                table: "Room",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "Pet_Service_Room",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "Pet_Service_Room",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "Hotel",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "Hotel",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.Sql(@"
                ALTER TABLE ""Appointments"" ADD COLUMN ""status_temp"" integer;
                UPDATE ""Appointments"" SET ""status_temp"" = 
                    CASE 
                        WHEN ""status"" = true THEN 1
                        WHEN ""status"" = false THEN 0
                        ELSE 0
                    END;
                ALTER TABLE ""Appointments"" DROP COLUMN ""status"";
                ALTER TABLE ""Appointments"" RENAME COLUMN ""status_temp"" TO ""status"";
                -- Set NOT NULL constraint and default value
                ALTER TABLE ""Appointments"" ALTER COLUMN ""status"" SET NOT NULL;
                ALTER TABLE ""Appointments"" ALTER COLUMN ""status"" SET DEFAULT 0;
            ");

            migrationBuilder.AlterColumn<string>(
                name: "location_type",
                table: "Appointments",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Room_hotel_id",
                table: "Room",
                column: "hotel_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Hotel_hotel_id",
                table: "Room",
                column: "hotel_id",
                principalTable: "Hotel",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Hotel_hotel_id",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Room_hotel_id",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "Room_Booking");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "Room_Booking");

            migrationBuilder.DropColumn(
                name: "hotel_id",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "Pet_Service_Room");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "Pet_Service_Room");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "Hotel");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "Hotel");

            migrationBuilder.RenameColumn(
                name: "end_time",
                table: "Appointments",
                newName: "end_type");

            migrationBuilder.AddColumn<Guid>(
                name: "Hotelid",
                table: "Room",
                type: "uuid",
                nullable: true);

            migrationBuilder.Sql(@"
                ALTER TABLE ""Appointments"" ADD COLUMN ""status_temp"" boolean;
                UPDATE ""Appointments"" SET ""status_temp"" = 
                    CASE 
                        WHEN ""status"" = 1 THEN true
                        WHEN ""status"" = 0 THEN false
                        ELSE null
                    END;
                ALTER TABLE ""Appointments"" DROP COLUMN ""status"";
                ALTER TABLE ""Appointments"" RENAME COLUMN ""status_temp"" TO ""status"";
            ");

            migrationBuilder.AlterColumn<string>(
                name: "location_type",
                table: "Appointments",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_Room_Hotelid",
                table: "Room",
                column: "Hotelid");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Hotel_Hotelid",
                table: "Room",
                column: "Hotelid",
                principalTable: "Hotel",
                principalColumn: "id");
        }
    }
}
