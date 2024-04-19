using GhostBank.Infrastructure.Data.Entities.Audit.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GhostBank.Infrastructure.Data.Mappings.Audit.Identity;

public class UserClaimAuditMap : BaseAuditMap<UserClaimAudit>
{
	public override void Configure(EntityTypeBuilder<UserClaimAudit> builder)
	{
		builder.ToTable(nameof(UserClaimAudit));

		builder
			.Property(x => x.UserClaimId)
			.HasColumnType("UNIQUEIDENTIFIER")
			.IsRequired();

		base.Configure(builder);
	}
}
