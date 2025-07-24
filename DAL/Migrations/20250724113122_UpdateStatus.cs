using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
               @"ALTER TABLE ""Room_Booking"" 
                  ALTER COLUMN status TYPE integer 
                  USING (CASE WHEN status THEN 1 ELSE 0 END); 
                  ALTER TABLE ""Room_Booking"" 
                  ALTER COLUMN status SET DEFAULT 1;"
           );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
               @"ALTER TABLE ""Room_Booking""
                  ALTER COLUMN status TYPE boolean
                  USING (CASE WHEN status = 1 THEN TRUE ELSE FALSE END);
                  ALTER TABLE ""Room_Booking""
                  ALTER COLUMN status SET DEFAULT FALSE;"
           );
        }
    }
}
