using GhostBank.Infrastructure.Data.Entities.Bank;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GhostBank.Infrastructure.Data.Mappings.Bank;

public class PixMap : BaseMap<Pix>
{
	public override void Configure(EntityTypeBuilder<Pix> builder)
	{
		builder.ToTable(nameof(Pix));

		builder
			.Property(x => x.Key)
			.HasColumnType("VARCHAR")
			.IsRequired();

		builder
			.Property(x => x.Limit)
			.HasColumnType("DECIMAL")
			.IsRequired();

		builder
			.Property(x => x.Type)
			.HasColumnType("VARCHAR")
			.IsRequired();

		builder
			.HasIndex(x => x.Key)
			.IsUnique();

		builder
			.HasOne(x => x.Account)
			.WithMany(x => x.Pix)
			.OnDelete(DeleteBehavior.NoAction);

		base.Configure(builder);
	}
}
