using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infastructure.Migrations
{
    /// <inheritdoc />
    public partial class db4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TestsJson",
                table: "ProblemEntity",
                newName: "OutputsJson");

            migrationBuilder.AddColumn<string>(
                name: "InputsJson",
                table: "ProblemEntity",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InputsJson",
                table: "ProblemEntity");

            migrationBuilder.RenameColumn(
                name: "OutputsJson",
                table: "ProblemEntity",
                newName: "TestsJson");
        }
    }
}
