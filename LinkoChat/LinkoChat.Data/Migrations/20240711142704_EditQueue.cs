using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkoChat.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditQueue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSearching",
                table: "Queues",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSearching",
                table: "Queues");
        }
    }
}
