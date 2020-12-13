using Microsoft.EntityFrameworkCore.Migrations;

namespace classifieds.Migrations
{
    public partial class dopostnmechage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deopsit",
                table: "Posts");

            migrationBuilder.AddColumn<double>(
                name: "Deposit",
                table: "Posts",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deposit",
                table: "Posts");

            migrationBuilder.AddColumn<double>(
                name: "Deopsit",
                table: "Posts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
