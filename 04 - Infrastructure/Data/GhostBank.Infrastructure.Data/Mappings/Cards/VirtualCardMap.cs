using GhostBank.Infrastructure.Data.Entities.Cards;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GhostBank.Infrastructure.Data.Mappings.Cards;

public class VirtualCardMap : BaseMap<VirtualCard>
{
	public override void Configure(EntityTypeBuilder<VirtualCard> builder)
	{
		builder.HasBaseType(typeof(Card));

		builder
			.Property(x => x.IsCredit)
			.HasColumnType("BIT")
			.IsRequired();

		builder
			.Property(x => x.Limit)
			.HasColumnType("DECIMAL")
			.HasDefaultValue(null)
			.IsRequired(false);
	}
}
