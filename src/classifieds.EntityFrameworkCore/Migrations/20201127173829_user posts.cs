using Microsoft.EntityFrameworkCore.Migrations;

namespace classifieds.Migrations
{
    public partial class userposts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Posts_CreatorUserId",
                table: "Posts",
                column: "CreatorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AbpUsers_CreatorUserId",
                table: "Posts",
                column: "CreatorUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AbpUsers_CreatorUserId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_CreatorUserId",
                table: "Posts");
        }
    }
}
