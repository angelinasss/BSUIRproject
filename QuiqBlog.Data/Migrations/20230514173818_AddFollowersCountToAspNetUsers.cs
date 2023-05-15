using Microsoft.EntityFrameworkCore.Migrations;

namespace QuiqBlog.Data.Migrations
{
    public partial class AddFollowersCountToAspNetUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
    name: "FollowersCount",
    table: "AspNetUsers",
    nullable: false,
    defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
