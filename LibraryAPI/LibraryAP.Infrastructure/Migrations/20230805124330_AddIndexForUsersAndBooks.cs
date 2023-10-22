using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexForUsersAndBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_Firstname_Lastname_Email",
                table: "Users",
                columns: new[] { "Firstname", "Lastname", "Email" });

            migrationBuilder.CreateIndex(
                name: "IX_Books_Title_Author",
                table: "Books",
                columns: new[] { "Title", "Author" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Firstname_Lastname_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Books_Title_Author",
                table: "Books");
        }
    }
}
