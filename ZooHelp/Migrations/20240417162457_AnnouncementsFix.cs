using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooHelp.Migrations
{
    /// <inheritdoc />
    public partial class AnnouncementsFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnimalName",
                table: "Announcements",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnimalName",
                table: "Announcements");
        }
    }
}
