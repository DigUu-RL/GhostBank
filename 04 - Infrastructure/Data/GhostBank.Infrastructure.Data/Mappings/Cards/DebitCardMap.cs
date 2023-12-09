using GhostBank.Infrastructure.Data.Entities.Cards;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GhostBank.Infrastructure.Data.Mappings.Cards;

public class DebitCardMap : BaseMap<DebitCard>
{
	public override void Configure(EntityTypeBuilder<DebitCard> builder)
	{
		builder.HasBaseType(typeof(Card));
	}
}
