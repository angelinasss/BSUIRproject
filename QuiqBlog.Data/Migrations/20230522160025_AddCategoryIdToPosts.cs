using Microsoft.EntityFrameworkCore.Migrations;

namespace QuiqBlog.Data.Migrations
{
    public partial class AddCategoryIdToPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                   name: "CategoryId",
                   table: "Posts",
                   nullable: false,
                   defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
               name: "CategoryId",
               table: "Posts");
        }
    }
}
