using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GhostBank.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class CardMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Card",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsCredit",
                table: "Card",
                type: "BIT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Limit",
                table: "Card",
                type: "DECIMAL(18,0)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "VirtualCard_Limit",
                table: "Card",
                type: "DECIMAL(18,0)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "DECIMAL(18,0)", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "DECIMAL(18,0)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    IsPaid = table.Column<bool>(type: "BIT", nullable: false),
                    CardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Excluded = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoice_Card_CardId",
                        column: x => x.CardId,
                        principalTable: "Card",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_CardId",
                table: "Invoice",
                column: "CardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "IsCredit",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "Limit",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "VirtualCard_Limit",
                table: "Card");
        }
    }
}
