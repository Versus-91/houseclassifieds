using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace classifieds.Migrations
{
    public partial class fav : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_AbpUsers_UserId1",
                table: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_UserId1",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Favorites");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Favorites",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "Favorites",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "Favorites",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "Favorites",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "Favorites");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Favorites",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "UserId1",
                table: "Favorites",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId1",
                table: "Favorites",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_AbpUsers_UserId1",
                table: "Favorites",
                column: "UserId1",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
