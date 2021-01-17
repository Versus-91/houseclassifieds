using Microsoft.EntityFrameworkCore.Migrations;

namespace classifieds.Migrations
{
    public partial class options : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_ReportOption_ReportOptionId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_ReportOption_ReportOptionId",
                table: "Reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReportOption",
                table: "ReportOption");

            migrationBuilder.RenameTable(
                name: "ReportOption",
                newName: "ReportOptions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReportOptions",
                table: "ReportOptions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_ReportOptions_ReportOptionId",
                table: "Posts",
                column: "ReportOptionId",
                principalTable: "ReportOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_ReportOptions_ReportOptionId",
                table: "Reports",
                column: "ReportOptionId",
                principalTable: "ReportOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_ReportOptions_ReportOptionId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_ReportOptions_ReportOptionId",
                table: "Reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReportOptions",
                table: "ReportOptions");

            migrationBuilder.RenameTable(
                name: "ReportOptions",
                newName: "ReportOption");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReportOption",
                table: "ReportOption",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_ReportOption_ReportOptionId",
                table: "Posts",
                column: "ReportOptionId",
                principalTable: "ReportOption",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_ReportOption_ReportOptionId",
                table: "Reports",
                column: "ReportOptionId",
                principalTable: "ReportOption",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
