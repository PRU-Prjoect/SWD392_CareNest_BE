using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class payment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BANK_ACCOUNT_NAME",
                table: "Account",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BANK_ACCOUNT_NO",
                table: "Account",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BANK_ID",
                table: "Account",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BANK_ACCOUNT_NAME",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "BANK_ACCOUNT_NO",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "BANK_ID",
                table: "Account");
        }
    }
}
