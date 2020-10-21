using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CreditScore.Persistence.Migrations
{
    public partial class DbInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserCreditScores",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(nullable: false),
                    UpdatedOnUtc = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: true),
                    IdentityNumber = table.Column<string>(nullable: true),
                    Score = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCreditScores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserCreditScoreHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(nullable: false),
                    UpdatedOnUtc = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: true),
                    Score = table.Column<int>(nullable: false),
                    UserCreditScoreId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCreditScoreHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCreditScoreHistories_UserCreditScores_UserCreditScoreId",
                        column: x => x.UserCreditScoreId,
                        principalTable: "UserCreditScores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCreditScoreHistories_UserCreditScoreId",
                table: "UserCreditScoreHistories",
                column: "UserCreditScoreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCreditScoreHistories");

            migrationBuilder.DropTable(
                name: "UserCreditScores");
        }
    }
}
