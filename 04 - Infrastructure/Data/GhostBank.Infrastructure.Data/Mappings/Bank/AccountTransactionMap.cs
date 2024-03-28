using GhostBank.Infrastructure.Data.Entities.Bank;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GhostBank.Infrastructure.Data.Mappings.Bank;

public class AccountTransactionMap : BaseMap<AccountTransaction>
{
	public override void Configure(EntityTypeBuilder<AccountTransaction> builder)
	{
		builder.ToTable(nameof(AccountTransaction));

		builder
			.HasOne(x => x.Transaction)
			.WithMany(x => x.Accounts)
			.HasForeignKey(x => x.TransactionId)
			.OnDelete(DeleteBehavior.NoAction);

		builder
			.HasOne(x => x.Account)
			.WithMany(x => x.Transactions)
			.HasForeignKey(x => x.AccountId)
			.OnDelete(DeleteBehavior.NoAction);

		base.Configure(builder);
	}
}
