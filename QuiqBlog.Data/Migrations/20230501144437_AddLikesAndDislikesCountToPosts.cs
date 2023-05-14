using Microsoft.EntityFrameworkCore.Migrations;

namespace QuiqBlog.Data.Migrations
{
    public partial class AddLikesAndDislikesCountToPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DislikesCount",
                table: "Posts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LikesCount",
                table: "Posts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DislikesCount",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "LikesCount",
                table: "Posts");
        }
    }
}
