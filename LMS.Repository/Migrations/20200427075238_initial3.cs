using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Repository.Migrations
{
    public partial class initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mayor_Cities_CityId",
                table: "Mayor");

            migrationBuilder.DropIndex(
                name: "IX_Mayor_CityId",
                table: "Mayor");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Mayor");

            migrationBuilder.AddColumn<int>(
                name: "CityIdString",
                table: "Mayor",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mayor_CityIdString",
                table: "Mayor",
                column: "CityIdString",
                unique: true,
                filter: "[CityIdString] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Mayor_Cities_CityIdString",
                table: "Mayor",
                column: "CityIdString",
                principalTable: "Cities",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mayor_Cities_CityIdString",
                table: "Mayor");

            migrationBuilder.DropIndex(
                name: "IX_Mayor_CityIdString",
                table: "Mayor");

            migrationBuilder.DropColumn(
                name: "CityIdString",
                table: "Mayor");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Mayor",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mayor_CityId",
                table: "Mayor",
                column: "CityId",
                unique: true,
                filter: "[CityId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Mayor_Cities_CityId",
                table: "Mayor",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
