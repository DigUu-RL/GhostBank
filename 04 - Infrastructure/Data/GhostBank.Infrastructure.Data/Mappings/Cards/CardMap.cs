using GhostBank.Infrastructure.Data.Entities.Cards;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GhostBank.Infrastructure.Data.Mappings.Cards;

public class CardMap : BaseMap<Card>
{
	public override void Configure(EntityTypeBuilder<Card> builder)
	{
		builder.ToTable(nameof(Card));

		builder
			.Property(x => x.Number)
			.HasColumnType("VARCHAR")
			.IsRequired();

		builder
			.Property(x => x.VerificationCode)
			.HasColumnType("VARCHAR")
			.IsRequired();

		builder
			.Property(x => x.ExpirationDate)
			.HasColumnType("DATETIME")
			.IsRequired();

		builder
			.Property(x => x.Password)
			.HasColumnType("VARCHAR")
			.IsRequired();

		builder
			.HasOne(x => x.Account)
			.WithMany(x => x.Cards)
			.HasForeignKey(x => x.AccountId)
			.OnDelete(DeleteBehavior.NoAction);

		base.Configure(builder);
	}
}
