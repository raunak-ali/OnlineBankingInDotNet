using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineBanking.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountUserProfiles",
                columns: table => new
                {
                    AccountUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    middle_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Father_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobilenumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    aadharnumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OccupationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SourceOfIncome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrossAnnualIncome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptedForDebitCard = table.Column<bool>(type: "bit", nullable: false),
                    OptedForNetBanking = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountUserProfiles", x => x.AccountUserId);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    TokenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountUserId = table.Column<int>(type: "int", nullable: false),
                    TokenValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.TokenId);
                });

            migrationBuilder.CreateTable(
                name: "PermanenetAddresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Landmark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pincode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMailingAddress = table.Column<bool>(type: "bit", nullable: false),
                    AccountUserId = table.Column<int>(type: "int", nullable: false),
                    AccountProfileAccountUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermanenetAddresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_PermanenetAddresses_AccountUserProfiles_AccountProfileAccountUserId",
                        column: x => x.AccountProfileAccountUserId,
                        principalTable: "AccountUserProfiles",
                        principalColumn: "AccountUserId");
                });

            migrationBuilder.CreateTable(
                name: "ResidentialAddresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Landmark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pincode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountUserId = table.Column<int>(type: "int", nullable: false),
                    AccountProfileAccountUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResidentialAddresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_ResidentialAddresses_AccountUserProfiles_AccountProfileAccountUserId",
                        column: x => x.AccountProfileAccountUserId,
                        principalTable: "AccountUserProfiles",
                        principalColumn: "AccountUserId");
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountUserId = table.Column<int>(type: "int", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    LoginPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isLocked = table.Column<bool>(type: "bit", nullable: false),
                    extra_info = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isConfirmedUserProfile = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserProfiles_AccountUserProfiles_AccountUserId",
                        column: x => x.AccountUserId,
                        principalTable: "AccountUserProfiles",
                        principalColumn: "AccountUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserProfileId = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountNumber);
                    table.ForeignKey(
                        name: "FK_Accounts_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserProfileId",
                table: "Accounts",
                column: "UserProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PermanenetAddresses_AccountProfileAccountUserId",
                table: "PermanenetAddresses",
                column: "AccountProfileAccountUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResidentialAddresses_AccountProfileAccountUserId",
                table: "ResidentialAddresses",
                column: "AccountProfileAccountUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_AccountUserId",
                table: "UserProfiles",
                column: "AccountUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "PermanenetAddresses");

            migrationBuilder.DropTable(
                name: "ResidentialAddresses");

            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "AccountUserProfiles");
        }
    }
}
