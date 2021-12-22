using Microsoft.EntityFrameworkCore.Migrations;

namespace BrAcademy.Data.Migrations
{
    public partial class ini2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstructorID",
                table: "EventInstructors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EventInstructors_InstructorID",
                table: "EventInstructors",
                column: "InstructorID");

            migrationBuilder.AddForeignKey(
                name: "FK_EventInstructors_Instructors_InstructorID",
                table: "EventInstructors",
                column: "InstructorID",
                principalTable: "Instructors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventInstructors_Instructors_InstructorID",
                table: "EventInstructors");

            migrationBuilder.DropIndex(
                name: "IX_EventInstructors_InstructorID",
                table: "EventInstructors");

            migrationBuilder.DropColumn(
                name: "InstructorID",
                table: "EventInstructors");
        }
    }
}
