using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooHelp.Migrations
{
    /// <inheritdoc />
    public partial class UserFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_Users_UserEmail",
                table: "Announcements");

            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_Users_UserEmail1",
                table: "Announcements");

            migrationBuilder.DropIndex(
                name: "IX_Announcements_UserEmail1",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "AuthorEmail",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "UserEmail1",
                table: "Announcements");

            migrationBuilder.AlterColumn<string>(
                name: "UserEmail",
                table: "Announcements",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_Users_UserEmail",
                table: "Announcements",
                column: "UserEmail",
                principalTable: "Users",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_Users_UserEmail",
                table: "Announcements");

            migrationBuilder.AlterColumn<string>(
                name: "UserEmail",
                table: "Announcements",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "AuthorEmail",
                table: "Announcements",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserEmail1",
                table: "Announcements",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_UserEmail1",
                table: "Announcements",
                column: "UserEmail1");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_Users_UserEmail",
                table: "Announcements",
                column: "UserEmail",
                principalTable: "Users",
                principalColumn: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_Users_UserEmail1",
                table: "Announcements",
                column: "UserEmail1",
                principalTable: "Users",
                principalColumn: "Email");
        }
    }
}
