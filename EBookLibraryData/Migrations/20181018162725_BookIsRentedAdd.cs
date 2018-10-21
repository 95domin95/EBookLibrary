using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EBookLibraryData.Migrations
{
    public partial class BookIsRentedAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Copies_AspNetUsers_UserId1",
                table: "Copies");

            migrationBuilder.DropForeignKey(
                name: "FK_Loan_Copies_CopyId",
                table: "Loan");

            migrationBuilder.DropForeignKey(
                name: "FK_Loan_AspNetUsers_UserId1",
                table: "Loan");

            migrationBuilder.DropIndex(
                name: "IX_Copies_UserId1",
                table: "Copies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Loan",
                table: "Loan");

            migrationBuilder.DropIndex(
                name: "IX_Loan_UserId1",
                table: "Loan");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Copies");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Loan");

            migrationBuilder.RenameTable(
                name: "Loan",
                newName: "Loans");

            migrationBuilder.RenameIndex(
                name: "IX_Loan_CopyId",
                table: "Loans",
                newName: "IX_Loans_CopyId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Copies",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<bool>(
                name: "IsRented",
                table: "Copies",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LoanDurationDays",
                table: "Copies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Copies",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Loans",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Loans",
                table: "Loans",
                column: "LoanId");

            migrationBuilder.CreateIndex(
                name: "IX_Copies_UserId",
                table: "Copies",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_UserId",
                table: "Loans",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Copies_AspNetUsers_UserId",
                table: "Copies",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Copies_CopyId",
                table: "Loans",
                column: "CopyId",
                principalTable: "Copies",
                principalColumn: "CopyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_AspNetUsers_UserId",
                table: "Loans",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Copies_AspNetUsers_UserId",
                table: "Copies");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Copies_CopyId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_AspNetUsers_UserId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Copies_UserId",
                table: "Copies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Loans",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_UserId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "IsRented",
                table: "Copies");

            migrationBuilder.DropColumn(
                name: "LoanDurationDays",
                table: "Copies");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Copies");

            migrationBuilder.RenameTable(
                name: "Loans",
                newName: "Loan");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_CopyId",
                table: "Loan",
                newName: "IX_Loan_CopyId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Copies",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Copies",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Loan",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Loan",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Loan",
                table: "Loan",
                column: "LoanId");

            migrationBuilder.CreateIndex(
                name: "IX_Copies_UserId1",
                table: "Copies",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_UserId1",
                table: "Loan",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Copies_AspNetUsers_UserId1",
                table: "Copies",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Loan_Copies_CopyId",
                table: "Loan",
                column: "CopyId",
                principalTable: "Copies",
                principalColumn: "CopyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loan_AspNetUsers_UserId1",
                table: "Loan",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
