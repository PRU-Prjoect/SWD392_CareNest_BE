using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class fixhotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotel_Sub_Address_sub_address_id",
                table: "Hotel");

            migrationBuilder.DropIndex(
                name: "IX_Hotel_sub_address_id",
                table: "Hotel");

            migrationBuilder.AlterColumn<int>(
                name: "total_room",
                table: "Hotel",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "sub_address_id",
                table: "Hotel",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Hotel",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "available_room",
                table: "Hotel",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_sub_address_id",
                table: "Hotel",
                column: "sub_address_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Hotel_Sub_Address_sub_address_id",
                table: "Hotel",
                column: "sub_address_id",
                principalTable: "Sub_Address",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotel_Sub_Address_sub_address_id",
                table: "Hotel");

            migrationBuilder.DropIndex(
                name: "IX_Hotel_sub_address_id",
                table: "Hotel");

            migrationBuilder.AlterColumn<int>(
                name: "total_room",
                table: "Hotel",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "sub_address_id",
                table: "Hotel",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Hotel",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "available_room",
                table: "Hotel",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_sub_address_id",
                table: "Hotel",
                column: "sub_address_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotel_Sub_Address_sub_address_id",
                table: "Hotel",
                column: "sub_address_id",
                principalTable: "Sub_Address",
                principalColumn: "id");
        }
    }
}
