using Microsoft.EntityFrameworkCore.Migrations;

namespace classifieds.Migrations
{
    public partial class imges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Districts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Citites",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Areas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Districts");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Citites");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Areas");
        }
    }
}
