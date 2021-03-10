using Microsoft.EntityFrameworkCore.Migrations;

namespace classifieds.Migrations
{
    public partial class media : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasMedia",
                table: "Posts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte>(
                name: "AdsCount",
                table: "AbpUsers",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasMedia",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "AdsCount",
                table: "AbpUsers");
        }
    }
}
