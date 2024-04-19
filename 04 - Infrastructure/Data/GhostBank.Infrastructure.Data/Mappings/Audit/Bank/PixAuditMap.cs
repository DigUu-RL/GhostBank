using GhostBank.Infrastructure.Data.Entities.Audit.Bank;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GhostBank.Infrastructure.Data.Mappings.Audit.Bank;

public class PixAuditMap : BaseAuditMap<PixAudit>
{
	public override void Configure(EntityTypeBuilder<PixAudit> builder)
	{
		builder.ToTable(nameof(PixAudit));

		builder
			.Property(x => x.PixId)
			.HasColumnType("UNIQUEIDENTIFIER")
			.IsRequired();

		base.Configure(builder);
	}
}
