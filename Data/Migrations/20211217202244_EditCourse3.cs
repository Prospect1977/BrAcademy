using Microsoft.EntityFrameworkCore.Migrations;

namespace BrAcademy.Data.Migrations
{
    public partial class EditCourse3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "Courses",
                newName: "Duration1");

            migrationBuilder.AddColumn<string>(
                name: "Duration2",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration2",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "Duration1",
                table: "Courses",
                newName: "Duration");
        }
    }
}
