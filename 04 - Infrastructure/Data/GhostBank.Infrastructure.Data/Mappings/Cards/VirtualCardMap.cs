using GhostBank.Infrastructure.Data.Entities.Cards;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GhostBank.Infrastructure.Data.Mappings.Cards;

public class VirtualCardMap : BaseMap<VirtualCard>
{
	public static void Configure(ModelBuilder modelBuilder)
	{
		EntityTypeBuilder<VirtualCard> builder = modelBuilder.Entity<VirtualCard>();

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
