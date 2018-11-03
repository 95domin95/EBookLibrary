using Microsoft.EntityFrameworkCore.Migrations;

namespace EBookLibraryData.Migrations
{
    public partial class FixesInLoanHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "LoanHistories",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoanHistories_UserId",
                table: "LoanHistories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoanHistories_AspNetUsers_UserId",
                table: "LoanHistories",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanHistories_AspNetUsers_UserId",
                table: "LoanHistories");

            migrationBuilder.DropIndex(
                name: "IX_LoanHistories_UserId",
                table: "LoanHistories");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "LoanHistories");
        }
    }
}
