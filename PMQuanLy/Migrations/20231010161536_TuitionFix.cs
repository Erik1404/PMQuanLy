using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMQuanLy.Migrations
{
    /// <inheritdoc />
    public partial class TuitionFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tuitions_Courses_CourseId",
                table: "Tuitions");

            migrationBuilder.DropIndex(
                name: "IX_Tuitions_CourseId",
                table: "Tuitions");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Tuitions");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Tuitions",
                newName: "TotalTuition");

            migrationBuilder.AddColumn<int>(
                name: "TuitionId",
                table: "CourseRegistrations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseRegistrations_TuitionId",
                table: "CourseRegistrations",
                column: "TuitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseRegistrations_Tuitions_TuitionId",
                table: "CourseRegistrations",
                column: "TuitionId",
                principalTable: "Tuitions",
                principalColumn: "TuitionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseRegistrations_Tuitions_TuitionId",
                table: "CourseRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_CourseRegistrations_TuitionId",
                table: "CourseRegistrations");

            migrationBuilder.DropColumn(
                name: "TuitionId",
                table: "CourseRegistrations");

            migrationBuilder.RenameColumn(
                name: "TotalTuition",
                table: "Tuitions",
                newName: "Amount");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Tuitions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tuitions_CourseId",
                table: "Tuitions",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tuitions_Courses_CourseId",
                table: "Tuitions",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
