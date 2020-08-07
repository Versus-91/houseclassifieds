using Microsoft.EntityFrameworkCore.Migrations;

namespace classifieds.Migrations
{
    public partial class ara_bdroom_post : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Area",
                table: "Posts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte>(
                name: "Bedroom",
                table: "Posts",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Area",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Bedroom",
                table: "Posts");
        }
    }
}
