using Microsoft.EntityFrameworkCore.Migrations;

namespace EBookLibraryData.Migrations
{
    public partial class RoleSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Copies_AspNetUsers_PatronId1",
                table: "Copies");

            migrationBuilder.DropForeignKey(
                name: "FK_Loan_AspNetUsers_PatronId1",
                table: "Loan");

            migrationBuilder.RenameColumn(
                name: "PatronId1",
                table: "Loan",
                newName: "UserId1");

            migrationBuilder.RenameColumn(
                name: "PatronId",
                table: "Loan",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Loan_PatronId1",
                table: "Loan",
                newName: "IX_Loan_UserId1");

            migrationBuilder.RenameColumn(
                name: "PatronId1",
                table: "Copies",
                newName: "UserId1");

            migrationBuilder.RenameColumn(
                name: "PatronId",
                table: "Copies",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Copies_PatronId1",
                table: "Copies",
                newName: "IX_Copies_UserId1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4b407668-a0e8-4207-8729-267f983c1793", "f2a03943-9fb2-4564-8bee-2f21b559dce5", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8029d390-5b14-4581-9d09-4e8e874ae801", "db93b1a2-db46-42fe-82c0-822b297ed9a2", "user", "user" });

            migrationBuilder.AddForeignKey(
                name: "FK_Copies_AspNetUsers_UserId1",
                table: "Copies",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Loan_AspNetUsers_UserId1",
                table: "Loan",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Copies_AspNetUsers_UserId1",
                table: "Copies");

            migrationBuilder.DropForeignKey(
                name: "FK_Loan_AspNetUsers_UserId1",
                table: "Loan");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "4b407668-a0e8-4207-8729-267f983c1793", "f2a03943-9fb2-4564-8bee-2f21b559dce5" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "8029d390-5b14-4581-9d09-4e8e874ae801", "db93b1a2-db46-42fe-82c0-822b297ed9a2" });

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Loan",
                newName: "PatronId1");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Loan",
                newName: "PatronId");

            migrationBuilder.RenameIndex(
                name: "IX_Loan_UserId1",
                table: "Loan",
                newName: "IX_Loan_PatronId1");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Copies",
                newName: "PatronId1");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Copies",
                newName: "PatronId");

            migrationBuilder.RenameIndex(
                name: "IX_Copies_UserId1",
                table: "Copies",
                newName: "IX_Copies_PatronId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Copies_AspNetUsers_PatronId1",
                table: "Copies",
                column: "PatronId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Loan_AspNetUsers_PatronId1",
                table: "Loan",
                column: "PatronId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
