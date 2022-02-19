using Microsoft.EntityFrameworkCore.Migrations;

namespace BrAcademy.Data.Migrations
{
    public partial class FullWidthImageCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullWidthCourseImageUrl",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullWidthCourseImageUrl",
                table: "Courses");
        }
    }
}
