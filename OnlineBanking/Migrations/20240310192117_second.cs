using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineBanking.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_UserProfiles_UserProfileId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_AccountUserProfiles_AccountUserId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_UserProfileId",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "UserProfileId",
                table: "Accounts",
                newName: "AccountUserProfileId");

            migrationBuilder.AlterColumn<string>(
                name: "AccountNumber",
                table: "UserProfiles",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                table: "AccountUserProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_AccountNumber",
                table: "UserProfiles",
                column: "AccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountUserProfileId",
                table: "Accounts",
                column: "AccountUserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AccountUserProfiles_AccountUserProfileId",
                table: "Accounts",
                column: "AccountUserProfileId",
                principalTable: "AccountUserProfiles",
                principalColumn: "AccountUserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_AccountUserProfiles_AccountUserId",
                table: "UserProfiles",
                column: "AccountUserId",
                principalTable: "AccountUserProfiles",
                principalColumn: "AccountUserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_Accounts_AccountNumber",
                table: "UserProfiles",
                column: "AccountNumber",
                principalTable: "Accounts",
                principalColumn: "AccountNumber",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AccountUserProfiles_AccountUserProfileId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_AccountUserProfiles_AccountUserId",
                table: "UserProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_Accounts_AccountNumber",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_AccountNumber",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_AccountUserProfileId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "AccountUserProfiles");

            migrationBuilder.RenameColumn(
                name: "AccountUserProfileId",
                table: "Accounts",
                newName: "UserProfileId");

            migrationBuilder.AlterColumn<string>(
                name: "AccountNumber",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserProfileId",
                table: "Accounts",
                column: "UserProfileId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_UserProfiles_UserProfileId",
                table: "Accounts",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_AccountUserProfiles_AccountUserId",
                table: "UserProfiles",
                column: "AccountUserId",
                principalTable: "AccountUserProfiles",
                principalColumn: "AccountUserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
