using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment.Migrations.Migrations
{
    public partial class InitialCategoryMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    AddedOn = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "AddedOn", "Type" },
                values: new object[] { new Guid("2fb2c391-0bc6-40a9-b85c-d5196864762e"), new DateTime(2023, 6, 20, 6, 17, 35, 51, DateTimeKind.Utc).AddTicks(7416), "Client Side Scripting" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "AddedOn", "Type" },
                values: new object[] { new Guid("5cce7a1e-34ae-4f2b-ab0b-415519a8b908"), new DateTime(2023, 6, 20, 6, 17, 35, 51, DateTimeKind.Utc).AddTicks(7411), "CRM" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "AddedOn", "Type" },
                values: new object[] { new Guid("5e7dcdbc-bdea-4805-b454-a8761823e1c9"), new DateTime(2023, 6, 20, 6, 17, 35, 51, DateTimeKind.Utc).AddTicks(7417), "Programming Language" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "AddedOn", "Type" },
                values: new object[] { new Guid("c6f9b020-bebf-4f5a-b880-201e12ab6b26"), new DateTime(2023, 6, 20, 6, 17, 35, 51, DateTimeKind.Utc).AddTicks(7414), "Cloud" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
