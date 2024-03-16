using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineBanking.Migrations
{
    /// <inheritdoc />
    public partial class f : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "middle_name",
                table: "AccountUserProfiles",
                newName: "Middle_name");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "AccountUserProfiles",
                newName: "Last_name");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "AccountUserProfiles",
                newName: "First_name");

            migrationBuilder.RenameColumn(
                name: "email_id",
                table: "AccountUserProfiles",
                newName: "Email_id");

            migrationBuilder.RenameColumn(
                name: "aadharnumber",
                table: "AccountUserProfiles",
                newName: "Aadharnumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Middle_name",
                table: "AccountUserProfiles",
                newName: "middle_name");

            migrationBuilder.RenameColumn(
                name: "Last_name",
                table: "AccountUserProfiles",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "First_name",
                table: "AccountUserProfiles",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "Email_id",
                table: "AccountUserProfiles",
                newName: "email_id");

            migrationBuilder.RenameColumn(
                name: "Aadharnumber",
                table: "AccountUserProfiles",
                newName: "aadharnumber");
        }
    }
}
