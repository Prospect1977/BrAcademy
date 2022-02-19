using Microsoft.EntityFrameworkCore.Migrations;

namespace BrAcademy.Data.Migrations
{
    public partial class DescDir : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HomePage",
                table: "Events");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionDirection",
                table: "Courses",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionDirection",
                table: "Courses");

            migrationBuilder.AddColumn<bool>(
                name: "HomePage",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
