using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkoChat.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditQueueTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Queues",
                newName: "SearchingFrom");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SearchingFrom",
                table: "Queues",
                newName: "CreationDate");
        }
    }
}
