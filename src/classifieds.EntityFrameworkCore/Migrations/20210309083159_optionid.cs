using Microsoft.EntityFrameworkCore.Migrations;

namespace classifieds.Migrations
{
    public partial class optionid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_ReportOptions_ReportOptionId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_ReportOptionId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ReportOptionId",
                table: "Posts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReportOptionId",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ReportOptionId",
                table: "Posts",
                column: "ReportOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_ReportOptions_ReportOptionId",
                table: "Posts",
                column: "ReportOptionId",
                principalTable: "ReportOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
