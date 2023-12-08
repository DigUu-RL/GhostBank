using GhostBank.Infrastructure.Data.Entities.Cards;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GhostBank.Infrastructure.Data.Mappings.Cards;

public class CreditCardMap : BaseMap<CreditCard>
{
	public override void Configure(EntityTypeBuilder<CreditCard> builder)
	{
		builder.HasBaseType(typeof(Card));

		builder
			.Property(x => x.Limit)
			.HasColumnType("DECIMAL")
			.IsRequired();

		base.Configure(builder);
	}
}
