using Microsoft.EntityFrameworkCore.Migrations;

namespace EBookLibraryData.Migrations
{
    public partial class RoleSeedv3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "35365d8c-430e-4d9a-8fe7-041c6237c331", "bdb737e9-d26b-43a1-a574-c1f12f150747" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "d2770c9b-33eb-4e4c-8528-c4e600212589", "7f13dc3d-e515-4954-80b9-083eb16688a9" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "admin", "09260e22-36c1-4726-972e-07a1ea2e662f", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4d1da35f-9789-4363-afd2-e2a2d80a19f9", "cc40d227-3a1c-4701-b2a4-3335e66ee0eb", "user", "user" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Adress", "City", "ConcurrencyStamp", "Country", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PostalCode", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "admin", 0, null, null, "83d31de7-2234-43fc-b9ab-3af0db621550", null, "admin@admin.com", true, false, null, "ADMIN@ADMIN.COM", "ADMIN", null, null, false, null, "2ac56b91-fff8-43a2-918a-9af1d1417431", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "admin", "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "4d1da35f-9789-4363-afd2-e2a2d80a19f9", "cc40d227-3a1c-4701-b2a4-3335e66ee0eb" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "admin", "admin" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "admin", "09260e22-36c1-4726-972e-07a1ea2e662f" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "admin", "83d31de7-2234-43fc-b9ab-3af0db621550" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d2770c9b-33eb-4e4c-8528-c4e600212589", "7f13dc3d-e515-4954-80b9-083eb16688a9", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "35365d8c-430e-4d9a-8fe7-041c6237c331", "bdb737e9-d26b-43a1-a574-c1f12f150747", "user", "user" });
        }
    }
}
