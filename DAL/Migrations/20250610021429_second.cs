using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageGallery_Service_service_id",
                table: "ImageGallery");

            migrationBuilder.DropForeignKey(
                name: "FK_Room_Pet_Type_pet_type_id",
                table: "Room");

            migrationBuilder.DropForeignKey(
                name: "FK_Room_Service_Type_service_type_id",
                table: "Room");

            migrationBuilder.DropForeignKey(
                name: "FK_Room_Shop_shop_id",
                table: "Room");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Pet_Type_pet_type_id",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_pet_type_id",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Room_pet_type_id",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Room_service_type_id",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Room_shop_id",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_ImageGallery_service_id",
                table: "ImageGallery");

            migrationBuilder.DropColumn(
                name: "pet_type_id",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "description",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "pet_type_id",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "service_type_id",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "shop_id",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "service_id",
                table: "ImageGallery");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Room",
                newName: "daily_price");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Room",
                newName: "amendities");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "Sub_Address",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "Hotelid",
                table: "Room",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_available",
                table: "Room",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "max_capacity",
                table: "Room",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "room_number",
                table: "Room",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "room_type",
                table: "Room",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "star",
                table: "Room",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "owner_id",
                table: "ImageGallery",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Hotel",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    total_room = table.Column<int>(type: "integer", nullable: true),
                    available_room = table.Column<int>(type: "integer", nullable: true),
                    shop_id = table.Column<Guid>(type: "uuid", nullable: false),
                    sub_address_id = table.Column<Guid>(type: "uuid", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotel", x => x.id);
                    table.ForeignKey(
                        name: "FK_Hotel_Shop_shop_id",
                        column: x => x.shop_id,
                        principalTable: "Shop",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hotel_Sub_Address_sub_address_id",
                        column: x => x.sub_address_id,
                        principalTable: "Sub_Address",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Pet_Service_Room",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    owner_id = table.Column<Guid>(type: "uuid", nullable: false),
                    pet_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_service = table.Column<bool>(type: "boolean", nullable: false),
                    service_id = table.Column<Guid>(type: "uuid", nullable: false),
                    room_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pet_Service_Room", x => x.id);
                    table.ForeignKey(
                        name: "FK_Pet_Service_Room_Pet_Type_pet_type_id",
                        column: x => x.pet_type_id,
                        principalTable: "Pet_Type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pet_Service_Room_Room_room_id",
                        column: x => x.room_id,
                        principalTable: "Room",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pet_Service_Room_Service_service_id",
                        column: x => x.service_id,
                        principalTable: "Service",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Room_Booking",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    room_detail_id = table.Column<Guid>(type: "uuid", nullable: false),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    check_in_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    check_out_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    total_night = table.Column<int>(type: "integer", nullable: false),
                    total_amount = table.Column<int>(type: "integer", nullable: false),
                    feeding_schedule = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    medication_schedule = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room_Booking", x => x.id);
                    table.ForeignKey(
                        name: "FK_Room_Booking_Customer_customer_id",
                        column: x => x.customer_id,
                        principalTable: "Customer",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Room_Booking_Room_room_detail_id",
                        column: x => x.room_detail_id,
                        principalTable: "Room",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Room_Hotelid",
                table: "Room",
                column: "Hotelid");

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_shop_id",
                table: "Hotel",
                column: "shop_id");

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_sub_address_id",
                table: "Hotel",
                column: "sub_address_id");

            migrationBuilder.CreateIndex(
                name: "IX_Pet_Service_Room_pet_type_id",
                table: "Pet_Service_Room",
                column: "pet_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_Pet_Service_Room_room_id",
                table: "Pet_Service_Room",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "IX_Pet_Service_Room_service_id",
                table: "Pet_Service_Room",
                column: "service_id");

            migrationBuilder.CreateIndex(
                name: "IX_Room_Booking_customer_id",
                table: "Room_Booking",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Room_Booking_room_detail_id",
                table: "Room_Booking",
                column: "room_detail_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Hotel_Hotelid",
                table: "Room",
                column: "Hotelid",
                principalTable: "Hotel",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Hotel_Hotelid",
                table: "Room");

            migrationBuilder.DropTable(
                name: "Hotel");

            migrationBuilder.DropTable(
                name: "Pet_Service_Room");

            migrationBuilder.DropTable(
                name: "Room_Booking");

            migrationBuilder.DropIndex(
                name: "IX_Room_Hotelid",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "name",
                table: "Sub_Address");

            migrationBuilder.DropColumn(
                name: "Hotelid",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "is_available",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "max_capacity",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "room_number",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "room_type",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "star",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "owner_id",
                table: "ImageGallery");

            migrationBuilder.RenameColumn(
                name: "daily_price",
                table: "Room",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "amendities",
                table: "Room",
                newName: "name");

            migrationBuilder.AddColumn<Guid>(
                name: "pet_type_id",
                table: "Service",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Room",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "pet_type_id",
                table: "Room",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "service_type_id",
                table: "Room",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "shop_id",
                table: "Room",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "service_id",
                table: "ImageGallery",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Service_pet_type_id",
                table: "Service",
                column: "pet_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_Room_pet_type_id",
                table: "Room",
                column: "pet_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_Room_service_type_id",
                table: "Room",
                column: "service_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_Room_shop_id",
                table: "Room",
                column: "shop_id");

            migrationBuilder.CreateIndex(
                name: "IX_ImageGallery_service_id",
                table: "ImageGallery",
                column: "service_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageGallery_Service_service_id",
                table: "ImageGallery",
                column: "service_id",
                principalTable: "Service",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Pet_Type_pet_type_id",
                table: "Room",
                column: "pet_type_id",
                principalTable: "Pet_Type",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Service_Type_service_type_id",
                table: "Room",
                column: "service_type_id",
                principalTable: "Service_Type",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Shop_shop_id",
                table: "Room",
                column: "shop_id",
                principalTable: "Shop",
                principalColumn: "account_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Pet_Type_pet_type_id",
                table: "Service",
                column: "pet_type_id",
                principalTable: "Pet_Type",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
