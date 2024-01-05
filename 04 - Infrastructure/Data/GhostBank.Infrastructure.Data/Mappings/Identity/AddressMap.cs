using GhostBank.Infrastructure.Data.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GhostBank.Infrastructure.Data.Mappings.Identity;

public class AddressMap : BaseMap<Address>
{
	public override void Configure(EntityTypeBuilder<Address> builder)
	{
		builder
			.Property(x => x.Street)
			.HasColumnType("VARCHAR")
			.HasMaxLength(255)
			.IsRequired();

		builder
			.Property(x => x.Number)
			.HasColumnType("VARCHAR")
			.HasMaxLength(255)
			.IsRequired(false);

		builder
			.Property(x => x.District)
			.HasColumnType("VARCHAR")
			.HasMaxLength(255)
			.IsRequired();
		
		builder
			.Property(x => x.City)
			.HasColumnType("VARCHAR")
			.HasMaxLength(255)
			.IsRequired();
		
		builder
			.Property(x => x.ZipCode)
			.HasColumnType("VARCHAR")
			.HasMaxLength(8)
			.IsRequired();

		builder
			.Property(x => x.State)
			.HasColumnType("VARCHAR")
			.HasMaxLength(255)
			.IsRequired();

		builder
			.HasOne(x => x.User)
			.WithOne(x => x.Address)
			.HasForeignKey<User>(x => x.AddressId)
			.OnDelete(DeleteBehavior.NoAction);

		base.Configure(builder);
	}
}
