using GhostBank.Infrastructure.Data.Entities.Bank;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GhostBank.Infrastructure.Data.Mappings.Bank;

public class TransactionMap : BaseMap<Transaction>
{
	public override void Configure(EntityTypeBuilder<Transaction> builder)
	{
		builder.ToTable(nameof(Transaction));

		builder
			.Property(x => x.Amount)
			.HasColumnType("DECIMAL")
			.IsRequired();

		builder
			.Property(x => x.Description)
			.HasColumnType("VARCHAR")
			.IsRequired(false);

		builder
			.Property(x => x.Type)
			.HasColumnType("VARCHAR")
			.IsRequired();

		builder
			.HasMany(x => x.Accounts)
			.WithOne(x => x.Transaction)
			.HasForeignKey(x => x.TransactionId)
			.OnDelete(DeleteBehavior.Cascade);

		base.Configure(builder);
	}
}
