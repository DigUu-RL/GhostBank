using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GhostBank.Infrastructure.Data.Migrations.GhostBankAudit
{
	/// <inheritdoc />
	public partial class AuditContext : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "UserLog",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Date = table.Column<DateTime>(type: "DATETIME", nullable: false),
					Column = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: true),
					OldValue = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: true),
					NewValue = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: true),
					Actor = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Action = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_UserLog", x => x.Id);
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "UserLog");
		}
	}
}
