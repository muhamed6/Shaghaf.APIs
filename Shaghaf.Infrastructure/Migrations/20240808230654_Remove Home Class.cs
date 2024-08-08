using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shaghaf.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveHomeClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_Homes_HomeId",
                table: "Advertisements");

            migrationBuilder.DropForeignKey(
                name: "FK_Birthdays_Homes_HomeId",
                table: "Birthdays");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Homes_HomeId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Memberships_Homes_HomeId",
                table: "Memberships");

            migrationBuilder.DropForeignKey(
                name: "FK_PhotoSessions_Homes_HomeId",
                table: "PhotoSessions");

            migrationBuilder.DropTable(
                name: "Homes");

            migrationBuilder.DropIndex(
                name: "IX_PhotoSessions_HomeId",
                table: "PhotoSessions");

            migrationBuilder.DropIndex(
                name: "IX_Memberships_HomeId",
                table: "Memberships");

            migrationBuilder.DropIndex(
                name: "IX_Categories_HomeId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Birthdays_HomeId",
                table: "Birthdays");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_HomeId",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "HomeId",
                table: "PhotoSessions");

            migrationBuilder.DropColumn(
                name: "HomeId",
                table: "Memberships");

            migrationBuilder.DropColumn(
                name: "HomeId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "HomeId",
                table: "Birthdays");

            migrationBuilder.DropColumn(
                name: "HomeId",
                table: "Advertisements");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HomeId",
                table: "PhotoSessions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HomeId",
                table: "Memberships",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HomeId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HomeId",
                table: "Birthdays",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HomeId",
                table: "Advertisements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Homes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    Heading = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Homes_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhotoSessions_HomeId",
                table: "PhotoSessions",
                column: "HomeId");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_HomeId",
                table: "Memberships",
                column: "HomeId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_HomeId",
                table: "Categories",
                column: "HomeId");

            migrationBuilder.CreateIndex(
                name: "IX_Birthdays_HomeId",
                table: "Birthdays",
                column: "HomeId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_HomeId",
                table: "Advertisements",
                column: "HomeId");

            migrationBuilder.CreateIndex(
                name: "IX_Homes_LocationId",
                table: "Homes",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_Homes_HomeId",
                table: "Advertisements",
                column: "HomeId",
                principalTable: "Homes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Birthdays_Homes_HomeId",
                table: "Birthdays",
                column: "HomeId",
                principalTable: "Homes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Homes_HomeId",
                table: "Categories",
                column: "HomeId",
                principalTable: "Homes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Memberships_Homes_HomeId",
                table: "Memberships",
                column: "HomeId",
                principalTable: "Homes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoSessions_Homes_HomeId",
                table: "PhotoSessions",
                column: "HomeId",
                principalTable: "Homes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
