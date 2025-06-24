using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class cart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    total = table.Column<double>(type: "double precision", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.id);
                    table.ForeignKey(
                        name: "FK_Cart_Customer_customer_id",
                        column: x => x.customer_id,
                        principalTable: "Customer",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Service_Cart",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    cart_id = table.Column<Guid>(type: "uuid", nullable: false),
                    service_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service_Cart", x => x.id);
                    table.ForeignKey(
                        name: "FK_Service_Cart_Cart_cart_id",
                        column: x => x.cart_id,
                        principalTable: "Cart",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Service_Cart_Service_service_id",
                        column: x => x.service_id,
                        principalTable: "Service",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_customer_id",
                table: "Cart",
                column: "customer_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Service_Cart_cart_id",
                table: "Service_Cart",
                column: "cart_id");

            migrationBuilder.CreateIndex(
                name: "IX_Service_Cart_service_id",
                table: "Service_Cart",
                column: "service_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Service_Cart");

            migrationBuilder.DropTable(
                name: "Cart");
        }
    }
}
