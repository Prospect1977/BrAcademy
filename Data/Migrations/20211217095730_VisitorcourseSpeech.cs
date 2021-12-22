using Microsoft.EntityFrameworkCore.Migrations;

namespace BrAcademy.Data.Migrations
{
    public partial class VisitorcourseSpeech : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InterestedOn",
                table: "VisitorCourses",
                newName: "InterestedDateTime");

            migrationBuilder.AddColumn<int>(
                name: "CountryID",
                table: "Visitors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Visitors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FlgDelete",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HomePage",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SortIndex",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "VisitorCourseSpeeches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpeechText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitorCourseID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitorCourseSpeeches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisitorCourseSpeeches_VisitorCourses_VisitorCourseID",
                        column: x => x.VisitorCourseID,
                        principalTable: "VisitorCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Visitors_CountryID",
                table: "Visitors",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_VisitorCourseSpeeches_VisitorCourseID",
                table: "VisitorCourseSpeeches",
                column: "VisitorCourseID");

            migrationBuilder.AddForeignKey(
                name: "FK_Visitors_Countries_CountryID",
                table: "Visitors",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visitors_Countries_CountryID",
                table: "Visitors");

            migrationBuilder.DropTable(
                name: "VisitorCourseSpeeches");

            migrationBuilder.DropIndex(
                name: "IX_Visitors_CountryID",
                table: "Visitors");

            migrationBuilder.DropColumn(
                name: "CountryID",
                table: "Visitors");

            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Visitors");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "FlgDelete",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "HomePage",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "SortIndex",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "InterestedDateTime",
                table: "VisitorCourses",
                newName: "InterestedOn");
        }
    }
}
