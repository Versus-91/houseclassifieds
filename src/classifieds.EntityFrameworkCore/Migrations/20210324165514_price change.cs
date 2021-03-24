using Microsoft.EntityFrameworkCore.Migrations;

namespace classifieds.Migrations
{
    public partial class pricechange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Deposit",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Rent",
                table: "Posts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Posts",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Deposit",
                table: "Posts",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Rent",
                table: "Posts",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
