using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineBanking.Migrations
{
    /// <inheritdoc />
    public partial class Ew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Paypees",
                columns: table => new
                {
                    PaypeeIdAccountNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PaypeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paypees", x => x.PaypeeIdAccountNumber);
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
                    AccountUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermanenetAddresses", x => x.AddressId);
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
                    AccountUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResidentialAddresses", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    TokenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OTPValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.TokenId);
                });

            migrationBuilder.CreateTable(
                name: "AccountUserProfiles",
                columns: table => new
                {
                    AccountUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    First_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Middle_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Last_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Father_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobilenumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aadharnumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PermanantAddressAddressId = table.Column<int>(type: "int", nullable: false),
                    ResidentialAddressAddressId = table.Column<int>(type: "int", nullable: false),
                    OccupationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SourceOfIncome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrossAnnualIncome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptedForDebitCard = table.Column<bool>(type: "bit", nullable: false),
                    OptedForNetBanking = table.Column<bool>(type: "bit", nullable: false),
                    ValidationDocsData = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountUserProfiles", x => x.AccountUserId);
                    table.ForeignKey(
                        name: "FK_AccountUserProfiles_PermanenetAddresses_PermanantAddressAddressId",
                        column: x => x.PermanantAddressAddressId,
                        principalTable: "PermanenetAddresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountUserProfiles_ResidentialAddresses_ResidentialAddressAddressId",
                        column: x => x.ResidentialAddressAddressId,
                        principalTable: "ResidentialAddresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountUserProfileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountNumber);
                    table.ForeignKey(
                        name: "FK_Accounts_AccountUserProfiles_AccountUserProfileId",
                        column: x => x.AccountUserProfileId,
                        principalTable: "AccountUserProfiles",
                        principalColumn: "AccountUserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrasactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Statement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PaypeeIdAccountNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountNumber1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_AccountNumber",
                        column: x => x.AccountNumber,
                        principalTable: "Accounts",
                        principalColumn: "AccountNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_AccountNumber1",
                        column: x => x.AccountNumber1,
                        principalTable: "Accounts",
                        principalColumn: "AccountNumber");
                    table.ForeignKey(
                        name: "FK_Transactions_Paypees_PaypeeIdAccountNumber",
                        column: x => x.PaypeeIdAccountNumber,
                        principalTable: "Paypees",
                        principalColumn: "PaypeeIdAccountNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: true),
                    LoginPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isLocked = table.Column<bool>(type: "bit", nullable: false),
                    extra_info = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isConfirmedUserProfile = table.Column<bool>(type: "bit", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserProfiles_AccountUserProfiles_AccountUserId",
                        column: x => x.AccountUserId,
                        principalTable: "AccountUserProfiles",
                        principalColumn: "AccountUserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Accounts_AccountNumber",
                        column: x => x.AccountNumber,
                        principalTable: "Accounts",
                        principalColumn: "AccountNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountUserProfileId",
                table: "Accounts",
                column: "AccountUserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountUserProfiles_PermanantAddressAddressId",
                table: "AccountUserProfiles",
                column: "PermanantAddressAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountUserProfiles_ResidentialAddressAddressId",
                table: "AccountUserProfiles",
                column: "ResidentialAddressAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountNumber",
                table: "Transactions",
                column: "AccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountNumber1",
                table: "Transactions",
                column: "AccountNumber1");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PaypeeIdAccountNumber",
                table: "Transactions",
                column: "PaypeeIdAccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_AccountNumber",
                table: "UserProfiles",
                column: "AccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_AccountUserId",
                table: "UserProfiles",
                column: "AccountUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "Paypees");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "AccountUserProfiles");

            migrationBuilder.DropTable(
                name: "PermanenetAddresses");

            migrationBuilder.DropTable(
                name: "ResidentialAddresses");
        }
    }
}
