using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JournalsAndAuth.Migrations
{
    public partial class AddBlogsToUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journal_AspNetUsers_UserId",
                table: "Journal");

            migrationBuilder.DropForeignKey(
                name: "FK_Journal_Blog_BlogId",
                table: "Journal");

            migrationBuilder.DropTable(
                name: "BlogUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Journal",
                table: "Journal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blog",
                table: "Blog");

            migrationBuilder.RenameTable(
                name: "Journal",
                newName: "Journals");

            migrationBuilder.RenameTable(
                name: "Blog",
                newName: "Blogs");

            migrationBuilder.RenameIndex(
                name: "IX_Journal_UserId",
                table: "Journals",
                newName: "IX_Journals_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Journal_BlogId",
                table: "Journals",
                newName: "IX_Journals_BlogId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Journals",
                table: "Journals",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserBlogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BlogId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBlogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBlogs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBlogs_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserBlogs_BlogId",
                table: "UserBlogs",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBlogs_UserId",
                table: "UserBlogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Journals_AspNetUsers_UserId",
                table: "Journals",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Journals_Blogs_BlogId",
                table: "Journals",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journals_AspNetUsers_UserId",
                table: "Journals");

            migrationBuilder.DropForeignKey(
                name: "FK_Journals_Blogs_BlogId",
                table: "Journals");

            migrationBuilder.DropTable(
                name: "UserBlogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Journals",
                table: "Journals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs");

            migrationBuilder.RenameTable(
                name: "Journals",
                newName: "Journal");

            migrationBuilder.RenameTable(
                name: "Blogs",
                newName: "Blog");

            migrationBuilder.RenameIndex(
                name: "IX_Journals_UserId",
                table: "Journal",
                newName: "IX_Journal_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Journals_BlogId",
                table: "Journal",
                newName: "IX_Journal_BlogId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Journal",
                table: "Journal",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blog",
                table: "Blog",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "BlogUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogUser_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogUser_Blog_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogUser_BlogId",
                table: "BlogUser",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogUser_UserId",
                table: "BlogUser",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Journal_AspNetUsers_UserId",
                table: "Journal",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Journal_Blog_BlogId",
                table: "Journal",
                column: "BlogId",
                principalTable: "Blog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
