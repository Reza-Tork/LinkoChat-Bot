using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkoChat.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateQueueGender : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Queues",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Queues");
        }
    }
}
