using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class listinguserrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "JobListings",
                newName: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JobListings_AppUserId",
                table: "JobListings",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobListings_Users_AppUserId",
                table: "JobListings",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobListings_Users_AppUserId",
                table: "JobListings");

            migrationBuilder.DropIndex(
                name: "IX_JobListings_AppUserId",
                table: "JobListings");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "JobListings",
                newName: "UserId");
        }
    }
}
