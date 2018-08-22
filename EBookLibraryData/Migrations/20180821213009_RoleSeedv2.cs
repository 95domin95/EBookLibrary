using Microsoft.EntityFrameworkCore.Migrations;

namespace EBookLibraryData.Migrations
{
    public partial class RoleSeedv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCategories_Subjects_CategoryId",
                table: "BookCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "4b407668-a0e8-4207-8729-267f983c1793", "f2a03943-9fb2-4564-8bee-2f21b559dce5" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "8029d390-5b14-4581-9d09-4e8e874ae801", "db93b1a2-db46-42fe-82c0-822b297ed9a2" });

            migrationBuilder.RenameTable(
                name: "Subjects",
                newName: "Categories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "CategoryId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d2770c9b-33eb-4e4c-8528-c4e600212589", "7f13dc3d-e515-4954-80b9-083eb16688a9", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "35365d8c-430e-4d9a-8fe7-041c6237c331", "bdb737e9-d26b-43a1-a574-c1f12f150747", "user", "user" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookCategories_Categories_CategoryId",
                table: "BookCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCategories_Categories_CategoryId",
                table: "BookCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "35365d8c-430e-4d9a-8fe7-041c6237c331", "bdb737e9-d26b-43a1-a574-c1f12f150747" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "d2770c9b-33eb-4e4c-8528-c4e600212589", "7f13dc3d-e515-4954-80b9-083eb16688a9" });

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Subjects");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects",
                column: "CategoryId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4b407668-a0e8-4207-8729-267f983c1793", "f2a03943-9fb2-4564-8bee-2f21b559dce5", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8029d390-5b14-4581-9d09-4e8e874ae801", "db93b1a2-db46-42fe-82c0-822b297ed9a2", "user", "user" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookCategories_Subjects_CategoryId",
                table: "BookCategories",
                column: "CategoryId",
                principalTable: "Subjects",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
