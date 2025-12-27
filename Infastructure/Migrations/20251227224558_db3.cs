using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infastructure.Migrations
{
    /// <inheritdoc />
    public partial class db3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Test1",
                table: "ProblemEntity");

            migrationBuilder.RenameColumn(
                name: "Test2",
                table: "ProblemEntity",
                newName: "TestsJson");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TestsJson",
                table: "ProblemEntity",
                newName: "Test2");

            migrationBuilder.AddColumn<string>(
                name: "Test1",
                table: "ProblemEntity",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
