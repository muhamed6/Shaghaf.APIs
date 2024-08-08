using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shaghaf.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRoomandLocationRelationShip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Rooms");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_LocationId",
                table: "Rooms",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Locations_LocationId",
                table: "Rooms",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Locations_LocationId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_LocationId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Rooms");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
