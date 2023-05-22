using Microsoft.EntityFrameworkCore.Migrations;

namespace QuiqBlog.Data.Migrations
{
    public partial class AddCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
    name: "Categories",
    columns: table => new
    {
        Id = table.Column<int>(nullable: false)
            .Annotation("SqlServer:Identity", "1, 1"),
        Title = table.Column<string>(nullable: true),
        Description = table.Column<string>(nullable: true)
    },
    constraints: table =>
    {
        table.PrimaryKey("PK_Categories", x => x.Id);
    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
