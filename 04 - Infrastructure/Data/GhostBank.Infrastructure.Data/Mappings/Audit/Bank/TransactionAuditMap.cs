using GhostBank.Infrastructure.Data.Entities.Audit.Bank;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GhostBank.Infrastructure.Data.Mappings.Audit.Bank;

public class TransactionAuditMap : BaseAuditMap<TransactionAudit>
{
	public override void Configure(EntityTypeBuilder<TransactionAudit> builder)
	{
		builder.ToTable(nameof(TransactionAudit));

		builder
			.Property(x => x.TransactionId)
			.HasColumnType("UNIQUEIDENTIFIER")
			.IsRequired();

		base.Configure(builder);
	}
}
