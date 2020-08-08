using Microsoft.EntityFrameworkCore.Migrations;

namespace classifieds.Migrations
{
    public partial class posttyoe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Posts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "View",
                table: "Posts",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_TypeId",
                table: "Posts",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_PropertyTypes_TypeId",
                table: "Posts",
                column: "TypeId",
                principalTable: "PropertyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_PropertyTypes_TypeId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_TypeId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "View",
                table: "Posts");
        }
    }
}
