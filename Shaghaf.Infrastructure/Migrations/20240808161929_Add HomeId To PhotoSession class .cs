using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shaghaf.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddHomeIdToPhotoSessionclass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoSessions_Homes_HomeId",
                table: "PhotoSessions");

            migrationBuilder.AlterColumn<int>(
                name: "HomeId",
                table: "PhotoSessions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoSessions_Homes_HomeId",
                table: "PhotoSessions",
                column: "HomeId",
                principalTable: "Homes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoSessions_Homes_HomeId",
                table: "PhotoSessions");

            migrationBuilder.AlterColumn<int>(
                name: "HomeId",
                table: "PhotoSessions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoSessions_Homes_HomeId",
                table: "PhotoSessions",
                column: "HomeId",
                principalTable: "Homes",
                principalColumn: "Id");
        }
    }
}
