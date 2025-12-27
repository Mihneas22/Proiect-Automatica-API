using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infastructure.Migrations
{
    /// <inheritdoc />
    public partial class db2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionEntity_UserEntity_UserId",
                table: "SubmissionEntity");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "SubmissionEntity",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "ProblemId",
                table: "SubmissionEntity",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ProblemEntity",
                columns: table => new
                {
                    ProblemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lab = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Requests = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Points = table.Column<int>(type: "int", nullable: true),
                    Test1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Test2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcceptanceRate = table.Column<double>(type: "float", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProblemEntity", x => x.ProblemId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionEntity_ProblemId",
                table: "SubmissionEntity",
                column: "ProblemId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionEntity_ProblemEntity_ProblemId",
                table: "SubmissionEntity",
                column: "ProblemId",
                principalTable: "ProblemEntity",
                principalColumn: "ProblemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionEntity_UserEntity_UserId",
                table: "SubmissionEntity",
                column: "UserId",
                principalTable: "UserEntity",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionEntity_ProblemEntity_ProblemId",
                table: "SubmissionEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionEntity_UserEntity_UserId",
                table: "SubmissionEntity");

            migrationBuilder.DropTable(
                name: "ProblemEntity");

            migrationBuilder.DropIndex(
                name: "IX_SubmissionEntity_ProblemId",
                table: "SubmissionEntity");

            migrationBuilder.DropColumn(
                name: "ProblemId",
                table: "SubmissionEntity");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "SubmissionEntity",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionEntity_UserEntity_UserId",
                table: "SubmissionEntity",
                column: "UserId",
                principalTable: "UserEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
