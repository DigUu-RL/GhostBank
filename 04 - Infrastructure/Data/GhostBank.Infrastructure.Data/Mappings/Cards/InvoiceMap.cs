using GhostBank.Infrastructure.Data.Entities.Cards;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GhostBank.Infrastructure.Data.Mappings.Cards;

public class InvoiceMap : BaseMap<Invoice>
{
	public override void Configure(EntityTypeBuilder<Invoice> builder)
	{
		builder
			.Property(x => x.Amount)
			.HasColumnType("DECIMAL")
			.IsRequired();

		builder
			.Property(x => x.PaidAmount)
			.HasColumnType("DECIMAL")
			.IsRequired();

		builder
			.Property(x => x.ExpirationDate)
			.HasColumnType("DATETIME")
			.IsRequired();

		builder
			.Property(x => x.IsPaid)
			.HasColumnType("BIT")
			.IsRequired();

		builder
			.HasOne(x => x.Card)
			.WithMany(x => x.Invoices)
			.HasForeignKey(x => x.CardId)
			.OnDelete(DeleteBehavior.NoAction);

		base.Configure(builder);
	}
}
