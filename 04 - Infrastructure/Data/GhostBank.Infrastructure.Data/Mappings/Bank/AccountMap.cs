using GhostBank.Infrastructure.Data.Entities.Bank;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GhostBank.Infrastructure.Data.Mappings.Bank;

public class AccountMap : BaseMap<Account>
{
	public override void Configure(EntityTypeBuilder<Account> builder)
	{
		builder.ToTable(nameof(Account));

		builder
			.Property(x => x.Agency)
			.HasColumnType("VARCHAR")
			.IsRequired();

		builder
			.Property(x => x.Number)
			.HasColumnType("VARCHAR")
			.IsRequired();

		builder
			.Property(x => x.Balance)
			.HasColumnType("DECIMAL")
			.IsRequired();

		builder
			.Property(x => x.Type)
			.HasColumnType("VARCHAR")
			.IsRequired();

		builder
			.HasIndex(x => x.Number)
			.IsUnique();

		builder
			.HasOne(x => x.User)
			.WithMany(x => x.Accounts)
			.HasForeignKey(x => x.UserId)
			.OnDelete(DeleteBehavior.NoAction);

		builder
			.HasMany(x => x.Pix)
			.WithOne(x => x.Account)
			.HasForeignKey(x => x.AccountId)
			.OnDelete(DeleteBehavior.Cascade);

		builder
			.HasMany(x => x.Cards)
			.WithOne(x => x.Account)
			.HasForeignKey(x => x.AccountId)
			.OnDelete(DeleteBehavior.Cascade);

		base.Configure(builder);
	}
}
