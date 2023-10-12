using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMQuanLy.Migrations
{
    /// <inheritdoc />
    public partial class DataStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusCourse",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "CourseStatus",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseStatus",
                table: "Courses");

            migrationBuilder.AddColumn<string>(
                name: "StatusCourse",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
