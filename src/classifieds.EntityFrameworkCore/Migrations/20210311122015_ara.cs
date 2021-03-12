using Microsoft.EntityFrameworkCore.Migrations;

namespace classifieds.Migrations
{
    public partial class ara : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Districts_Areas_AreaId",
                table: "Districts");

            migrationBuilder.DropForeignKey(
                name: "FK_Districts_Citites_CityId",
                table: "Districts");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Districts",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AreaId",
                table: "Districts",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_Areas_AreaId",
                table: "Districts",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_Citites_CityId",
                table: "Districts",
                column: "CityId",
                principalTable: "Citites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Districts_Areas_AreaId",
                table: "Districts");

            migrationBuilder.DropForeignKey(
                name: "FK_Districts_Citites_CityId",
                table: "Districts");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Districts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AreaId",
                table: "Districts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_Areas_AreaId",
                table: "Districts",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_Citites_CityId",
                table: "Districts",
                column: "CityId",
                principalTable: "Citites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
