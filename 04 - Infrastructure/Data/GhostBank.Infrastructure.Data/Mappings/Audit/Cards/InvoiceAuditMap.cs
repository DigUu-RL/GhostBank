using GhostBank.Infrastructure.Data.Entities.Audit.Cards;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GhostBank.Infrastructure.Data.Mappings.Audit.Cards;

public class InvoiceAuditMap : BaseAuditMap<InvoiceAudit>
{
	public override void Configure(EntityTypeBuilder<InvoiceAudit> builder)
	{
		builder.ToTable(nameof(InvoiceAudit));

		builder
			.Property(x => x.InvoiceId)
			.HasColumnType("UNIQUEIDENTIFIER")
			.IsRequired();

		base.Configure(builder);
	}
}
