using GhostBank.Infrastructure.Data.Entities.Cards;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GhostBank.Infrastructure.Data.Mappings.Cards;

public class DebitCardMap
{
	public static void Configure(ModelBuilder modelBuilder)
	{
		EntityTypeBuilder<DebitCard> builder = modelBuilder.Entity<DebitCard>();

		builder.HasBaseType(typeof(Card));
	}
}
