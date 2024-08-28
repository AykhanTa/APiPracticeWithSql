using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiPractice.DAL.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "48426da9-2602-4e73-bb4b-694f0172c385", null, "Super-Admin", "SUPER-USER" },
                    { "9fe63926-2677-475e-9523-12100512cdce", null, "User", "USER" },
                    { "ceedbf72-87bb-47f1-af7c-6a248a1a80e4", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ab57355c-9dfa-497b-b134-8e065f3c6273", 0, "f572d639-3f40-4f04-8244-8882c2a53413", "admin@example.com", true, null, false, null, "ADMIN@EXAMPLE.COM", "ADMINUSER", "AQAAAAIAAYagAAAAEHCCqjzdK0GqeZ6ADivjFIGw6tuWPSlLTHh3QB6l5+qor5AzwqvVaIdp+o/k2FbqBA==", null, false, "bcef2dc2-894d-42d8-a437-25387bb04ebe", false, "adminuser" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "ceedbf72-87bb-47f1-af7c-6a248a1a80e4", "ab57355c-9dfa-497b-b134-8e065f3c6273" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "48426da9-2602-4e73-bb4b-694f0172c385");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9fe63926-2677-475e-9523-12100512cdce");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ceedbf72-87bb-47f1-af7c-6a248a1a80e4", "ab57355c-9dfa-497b-b134-8e065f3c6273" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ceedbf72-87bb-47f1-af7c-6a248a1a80e4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab57355c-9dfa-497b-b134-8e065f3c6273");
        }
    }
}
