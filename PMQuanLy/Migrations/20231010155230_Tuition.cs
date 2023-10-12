using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMQuanLy.Migrations
{
    /// <inheritdoc />
    public partial class Tuition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tuitions_CourseId",
                table: "Tuitions",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Tuitions_StudentId",
                table: "Tuitions",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tuitions_Courses_CourseId",
                table: "Tuitions",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tuitions_Users_StudentId",
                table: "Tuitions",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tuitions_Courses_CourseId",
                table: "Tuitions");

            migrationBuilder.DropForeignKey(
                name: "FK_Tuitions_Users_StudentId",
                table: "Tuitions");

            migrationBuilder.DropIndex(
                name: "IX_Tuitions_CourseId",
                table: "Tuitions");

            migrationBuilder.DropIndex(
                name: "IX_Tuitions_StudentId",
                table: "Tuitions");
        }
    }
}
