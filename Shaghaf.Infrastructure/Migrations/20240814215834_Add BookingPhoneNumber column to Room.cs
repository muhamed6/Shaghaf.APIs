using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shaghaf.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingPhoneNumbercolumntoRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookingPhoneNumber",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingPhoneNumber",
                table: "Rooms");
        }
    }
}
