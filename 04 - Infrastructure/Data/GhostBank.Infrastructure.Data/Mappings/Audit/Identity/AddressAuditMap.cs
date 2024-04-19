using GhostBank.Infrastructure.Data.Entities.Audit.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GhostBank.Infrastructure.Data.Mappings.Audit.Identity;

public class AddressAuditMap : BaseAuditMap<AddressAudit>
{
	public override void Configure(EntityTypeBuilder<AddressAudit> builder)
	{
		builder.ToTable(nameof(AddressAudit));

		builder
			.Property(x => x.AdressId)
			.HasColumnType("UNIQUEIDENTIFIER")
			.IsRequired();

		base.Configure(builder);
	}
}
