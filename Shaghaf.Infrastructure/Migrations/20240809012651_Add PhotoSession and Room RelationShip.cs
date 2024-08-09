using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shaghaf.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPhotoSessionandRoomRelationShip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "PhotoSessions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PhotoSessions_RoomId",
                table: "PhotoSessions",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoSessions_Rooms_RoomId",
                table: "PhotoSessions",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoSessions_Rooms_RoomId",
                table: "PhotoSessions");

            migrationBuilder.DropIndex(
                name: "IX_PhotoSessions_RoomId",
                table: "PhotoSessions");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "PhotoSessions");
        }
    }
}
