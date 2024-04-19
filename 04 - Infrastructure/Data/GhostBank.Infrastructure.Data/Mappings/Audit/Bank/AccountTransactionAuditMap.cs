using GhostBank.Infrastructure.Data.Entities.Audit.Bank;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GhostBank.Infrastructure.Data.Mappings.Audit.Bank;

public class AccountTransactionAuditMap : BaseAuditMap<AccountTransationAudit>
{
	public override void Configure(EntityTypeBuilder<AccountTransationAudit> builder)
	{
		builder.ToTable(nameof(AccountTransationAudit));

		builder
			.Property(x => x.AccountTransactionId)
			.HasColumnType("UNIQUEIDENTIFIER")
			.IsRequired();

		base.Configure(builder);
	}
}
