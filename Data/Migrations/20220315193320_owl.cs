using Microsoft.EntityFrameworkCore.Migrations;

namespace BrAcademy.Data.Migrations
{
    public partial class owl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OwlCarousels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    SortIndex = table.Column<int>(type: "int", nullable: false),
                    CustomText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwlCarousels", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OwlCarousels");
        }
    }
}
