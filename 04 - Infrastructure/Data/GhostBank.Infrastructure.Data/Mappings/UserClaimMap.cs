using GhostBank.Infrastructure.Data.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GhostBank.Infrastructure.Data.Mappings;

public class UserClaimMap : BaseMap<UserClaim>
{
	public override void Configure(EntityTypeBuilder<UserClaim> builder)
	{
		base.Configure(builder);

		builder.ToTable(nameof(UserClaim));

		builder
			.Property(x => x.Type)
			.HasColumnType("VARCHAR")
			.HasMaxLength(255)
			.IsRequired();

		builder
			.Property(x => x.Value)
			.HasColumnType("VARCHAR")
			.HasMaxLength(255)
			.IsRequired();

		builder
			.HasOne(x => x.User)
			.WithMany(x => x.Claims)
			.OnDelete(DeleteBehavior.ClientNoAction);
	}
}
