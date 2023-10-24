using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMQuanLy.Migrations
{
    /// <inheritdoc />
    public partial class Two : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Scores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Scores",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Scores");
        }
    }
}
