using Microsoft.EntityFrameworkCore.Migrations;

namespace QuiqBlog.Data.Migrations
{
    public partial class AddPostType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_PostType_PostTypeId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostType",
                table: "PostType");

            migrationBuilder.RenameTable(
                name: "PostType",
                newName: "PostTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostTypes",
                table: "PostTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_PostTypes_PostTypeId",
                table: "Posts",
                column: "PostTypeId",
                principalTable: "PostTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_PostTypes_PostTypeId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostTypes",
                table: "PostTypes");

            migrationBuilder.RenameTable(
                name: "PostTypes",
                newName: "PostType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostType",
                table: "PostType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_PostType_PostTypeId",
                table: "Posts",
                column: "PostTypeId",
                principalTable: "PostType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
