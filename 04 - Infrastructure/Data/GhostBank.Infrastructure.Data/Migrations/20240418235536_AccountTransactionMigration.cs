using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GhostBank.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AccountTransactionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "User",
                newName: "Name");

            migrationBuilder.AddColumn<bool>(
                name: "IsParcialPaid",
                table: "Invoice",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "DECIMAL(18,0)", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR", nullable: true),
                    Type = table.Column<string>(type: "VARCHAR", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Excluded = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountTransaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Excluded = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountTransaction_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountTransaction_Transaction_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transaction",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountTransaction_AccountId",
                table: "AccountTransaction",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTransaction_TransactionId",
                table: "AccountTransaction",
                column: "TransactionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountTransaction");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropColumn(
                name: "IsParcialPaid",
                table: "Invoice");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "User",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "User",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
