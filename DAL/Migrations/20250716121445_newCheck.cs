using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class newCheck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "img_url",
                table: "Service",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "img_url_id",
                table: "Service",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "img_url",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "img_url_id",
                table: "Service");
        }
    }
}
