using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooHelp.Migrations
{
    /// <inheritdoc />
    public partial class AnnouncementsTypeFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Announcements",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Announcements");
        }
    }
}
