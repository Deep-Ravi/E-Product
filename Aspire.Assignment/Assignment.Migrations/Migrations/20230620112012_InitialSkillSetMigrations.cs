using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment.Migrations.Migrations
{
    public partial class InitialSkillSetMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SkillSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Proficiency = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    YearsOfExperience = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Achievement = table.Column<string>(type: "TEXT", nullable: true),
                    LastWorkedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ApprovalStatus = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    IsSendForApproval = table.Column<bool>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SkillId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    AddedOn = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkillSet_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkillSet_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkillSet_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SkillSet_CategoryId",
                table: "SkillSet",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillSet_SkillId",
                table: "SkillSet",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillSet_UserId",
                table: "SkillSet",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SkillSet");           
        }
    }
}
