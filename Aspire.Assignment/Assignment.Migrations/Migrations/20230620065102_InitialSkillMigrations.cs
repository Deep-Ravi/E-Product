using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment.Migrations.Migrations
{
    public partial class InitialSkillMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AddedOn = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skill_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "AddedOn", "CategoryId", "Name" },
                values: new object[] { new Guid("08e5a731-c969-488c-8526-1acb2ee27254"), new DateTime(2023, 6, 20, 6, 51, 2, 205, DateTimeKind.Utc).AddTicks(4113), new Guid("5cce7a1e-34ae-4f2b-ab0b-415519a8b908"), "Apex" });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "AddedOn", "CategoryId", "Name" },
                values: new object[] { new Guid("09b8f8e0-d431-4118-a2d4-a9343cf10033"), new DateTime(2023, 6, 20, 6, 51, 2, 205, DateTimeKind.Utc).AddTicks(4077), new Guid("2fb2c391-0bc6-40a9-b85c-d5196864762e"), "JQuery" });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "AddedOn", "CategoryId", "Name" },
                values: new object[] { new Guid("1701dc57-ccb9-403e-bdc2-b3bc460c95ac"), new DateTime(2023, 6, 20, 6, 51, 2, 205, DateTimeKind.Utc).AddTicks(4069), new Guid("2fb2c391-0bc6-40a9-b85c-d5196864762e"), "Angular" });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "AddedOn", "CategoryId", "Name" },
                values: new object[] { new Guid("241fd9c6-50e0-4d8c-90ee-a295e58ec9ba"), new DateTime(2023, 6, 20, 6, 51, 2, 205, DateTimeKind.Utc).AddTicks(4073), new Guid("2fb2c391-0bc6-40a9-b85c-d5196864762e"), "React JS" });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "AddedOn", "CategoryId", "Name" },
                values: new object[] { new Guid("392012d0-c131-4377-ac84-b2362836fb27"), new DateTime(2023, 6, 20, 6, 51, 2, 205, DateTimeKind.Utc).AddTicks(4098), new Guid("5e7dcdbc-bdea-4805-b454-a8761823e1c9"), "CSharp" });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "AddedOn", "CategoryId", "Name" },
                values: new object[] { new Guid("3c27b4ee-3273-41a5-a8e8-33afe6690d54"), new DateTime(2023, 6, 20, 6, 51, 2, 205, DateTimeKind.Utc).AddTicks(4110), new Guid("c6f9b020-bebf-4f5a-b880-201e12ab6b26"), "Terraform" });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "AddedOn", "CategoryId", "Name" },
                values: new object[] { new Guid("6cb033f9-f405-44f4-b262-2cb393004309"), new DateTime(2023, 6, 20, 6, 51, 2, 205, DateTimeKind.Utc).AddTicks(4101), new Guid("5e7dcdbc-bdea-4805-b454-a8761823e1c9"), "VB.NET" });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "AddedOn", "CategoryId", "Name" },
                values: new object[] { new Guid("933e4168-7b24-4045-b144-5297a4daf851"), new DateTime(2023, 6, 20, 6, 51, 2, 205, DateTimeKind.Utc).AddTicks(4079), new Guid("5e7dcdbc-bdea-4805-b454-a8761823e1c9"), "Java" });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "AddedOn", "CategoryId", "Name" },
                values: new object[] { new Guid("cd037d12-7feb-46fd-bf24-2489080fb39b"), new DateTime(2023, 6, 20, 6, 51, 2, 205, DateTimeKind.Utc).AddTicks(4104), new Guid("c6f9b020-bebf-4f5a-b880-201e12ab6b26"), "AWS" });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "AddedOn", "CategoryId", "Name" },
                values: new object[] { new Guid("cdc6d211-4822-46b6-a31b-a859d4a77cea"), new DateTime(2023, 6, 20, 6, 51, 2, 205, DateTimeKind.Utc).AddTicks(4119), new Guid("5cce7a1e-34ae-4f2b-ab0b-415519a8b908"), "SAP CRM" });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "AddedOn", "CategoryId", "Name" },
                values: new object[] { new Guid("e398c01e-b30d-4aea-9bf3-85a0d7f7ff9a"), new DateTime(2023, 6, 20, 6, 51, 2, 205, DateTimeKind.Utc).AddTicks(4107), new Guid("c6f9b020-bebf-4f5a-b880-201e12ab6b26"), "Azure" });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "AddedOn", "CategoryId", "Name" },
                values: new object[] { new Guid("fea067f2-a2c8-49b3-8746-38c1b184eced"), new DateTime(2023, 6, 20, 6, 51, 2, 205, DateTimeKind.Utc).AddTicks(4116), new Guid("5cce7a1e-34ae-4f2b-ab0b-415519a8b908"), "MS Dynamics CRM" });

            migrationBuilder.CreateIndex(
                name: "IX_Skill_CategoryId",
                table: "Skill",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Skill");
        }
    }
}
