using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkoChat.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditInChatColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInChat",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "IsEnded",
                table: "Chats",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnded",
                table: "Chats");

            migrationBuilder.AddColumn<bool>(
                name: "IsInChat",
                table: "Users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
