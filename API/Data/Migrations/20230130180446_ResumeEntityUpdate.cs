using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class ResumeEntityUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "Resume");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Resume");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Resume");

            migrationBuilder.AddColumn<byte[]>(
                name: "ResumeContents",
                table: "Resume",
                type: "BLOB",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResumeContents",
                table: "Resume");

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "Resume",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "Resume",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Resume",
                type: "TEXT",
                nullable: true);
        }
    }
}
