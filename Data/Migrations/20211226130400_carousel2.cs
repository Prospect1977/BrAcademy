using Microsoft.EntityFrameworkCore.Migrations;

namespace BrAcademy.Data.Migrations
{
    public partial class carousel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarouselVMId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CarouselVMId",
                table: "CourseCategories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CarouselVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    SortIndex = table.Column<int>(type: "int", nullable: false),
                    CustomText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarouselVM", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CarouselVMId",
                table: "Courses",
                column: "CarouselVMId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseCategories_CarouselVMId",
                table: "CourseCategories",
                column: "CarouselVMId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseCategories_CarouselVM_CarouselVMId",
                table: "CourseCategories",
                column: "CarouselVMId",
                principalTable: "CarouselVM",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CarouselVM_CarouselVMId",
                table: "Courses",
                column: "CarouselVMId",
                principalTable: "CarouselVM",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseCategories_CarouselVM_CarouselVMId",
                table: "CourseCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CarouselVM_CarouselVMId",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "CarouselVM");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CarouselVMId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_CourseCategories_CarouselVMId",
                table: "CourseCategories");

            migrationBuilder.DropColumn(
                name: "CarouselVMId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CarouselVMId",
                table: "CourseCategories");
        }
    }
}
