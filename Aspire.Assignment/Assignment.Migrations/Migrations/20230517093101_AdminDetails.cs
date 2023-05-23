using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment.Migrations.Migrations
{
    public partial class AdminDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AddedOn", "Email", "OperationId", "Password", "RoleId", "Username" },
                values: new object[] { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin@ayi.com", 1, "AQAAAAEAACcQAAAAEK7fK9rklmNwB8J395U3LgJhevQEwGd/RazdOjguuNDbCnNIFoP9V8fq5hxckoPQew==", 1, "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
