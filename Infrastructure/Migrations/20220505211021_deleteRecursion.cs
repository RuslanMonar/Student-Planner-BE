using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class deleteRecursion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Groups_GroupdId",
                table: "Projects");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Groups_GroupdId",
                table: "Projects",
                column: "GroupdId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Groups_GroupdId",
                table: "Projects");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Groups_GroupdId",
                table: "Projects",
                column: "GroupdId",
                principalTable: "Groups",
                principalColumn: "Id");
        }
    }
}
