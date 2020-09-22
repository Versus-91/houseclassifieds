using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace classifieds.Migrations
{
    public partial class postuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "Posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "Posts");
        }
    }
}
