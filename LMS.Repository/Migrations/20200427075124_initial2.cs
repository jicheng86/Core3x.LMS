using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Repository.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mayor_Cities_CityId",
                table: "Mayor");

            migrationBuilder.DropIndex(
                name: "IX_Mayor_CityId",
                table: "Mayor");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Mayor",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mayor_Cities_CityId",
                table: "Mayor");

            migrationBuilder.DropIndex(
                name: "IX_Mayor_CityId",
                table: "Mayor");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Mayor",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mayor_CityId",
                table: "Mayor",
                column: "CityId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Mayor_Cities_CityId",
                table: "Mayor",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
