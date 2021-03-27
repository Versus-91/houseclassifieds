using Microsoft.EntityFrameworkCore.Migrations;

namespace classifieds.Migrations
{
    public partial class agenid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RealEstateId",
                table: "Posts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_RealEstateId",
                table: "Posts",
                column: "RealEstateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_RealEstates_RealEstateId",
                table: "Posts",
                column: "RealEstateId",
                principalTable: "RealEstates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_RealEstates_RealEstateId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_RealEstateId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "RealEstateId",
                table: "Posts");
        }
    }
}
