using GhostBank.Infrastructure.Data.Entities.Audit.Cards;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GhostBank.Infrastructure.Data.Mappings.Audit.Cards;

public class CardAuditMap : BaseAuditMap<CardAudit>
{
	public override void Configure(EntityTypeBuilder<CardAudit> builder)
	{
		builder.ToTable(nameof(CardAudit));

		builder
			.Property(x => x.CardId)
			.HasColumnType("UNIQUEIDENTIFIER")
			.IsRequired();

		base.Configure(builder);
	}
}
