using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shaghaf.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBirthdayAndRoomRelationShip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Birthdays",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Birthdays_RoomId",
                table: "Birthdays",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Birthdays_Rooms_RoomId",
                table: "Birthdays",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Birthdays_Rooms_RoomId",
                table: "Birthdays");

            migrationBuilder.DropIndex(
                name: "IX_Birthdays_RoomId",
                table: "Birthdays");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Birthdays");
        }
    }
}
