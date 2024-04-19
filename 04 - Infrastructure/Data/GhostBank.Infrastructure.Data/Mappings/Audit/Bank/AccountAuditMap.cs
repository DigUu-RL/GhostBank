using GhostBank.Infrastructure.Data.Entities.Audit.Bank;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GhostBank.Infrastructure.Data.Mappings.Audit.Bank;

public class AccountAuditMap : BaseAuditMap<AccountAudit>
{
	public override void Configure(EntityTypeBuilder<AccountAudit> builder)
	{
		builder.ToTable(nameof(AccountAudit));

		builder
			.Property(x => x.AccountId)
			.HasColumnType("UNIQUEIDENTIFIER")
			.IsRequired();

		base.Configure(builder);
	}
}
