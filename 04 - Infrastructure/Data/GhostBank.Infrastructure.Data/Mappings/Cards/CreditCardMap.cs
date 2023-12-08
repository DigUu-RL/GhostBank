using GhostBank.Infrastructure.Data.Entities.Cards;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GhostBank.Infrastructure.Data.Mappings.Cards;

public class CreditCardMap
{
    public static void Configure(ModelBuilder modelBuilder)
	{
		EntityTypeBuilder<CreditCard> builder = modelBuilder.Entity<CreditCard>();

		builder.HasBaseType(typeof(Card));

		builder
			.Property(x => x.Limit)
			.HasColumnType("DECIMAL")
			.IsRequired();
	}
}
