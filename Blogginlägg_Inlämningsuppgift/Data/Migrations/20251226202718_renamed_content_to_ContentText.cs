using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blogginlägg_Inlämningsuppgift.Data.migrations
{
    /// <inheritdoc />
    public partial class renamed_content_to_ContentText : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Posts",
                newName: "ContentText");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContentText",
                table: "Posts",
                newName: "Content");
        }
    }
}
