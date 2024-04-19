using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GhostBank.Infrastructure.Data.Migrations.GhostBankAudit
{
    /// <inheritdoc />
    public partial class AccountTransactionAuditMigraton : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "UserAudit",
                type: "UNIQUEIDENTIFIER",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserAudit");
        }
    }
}
