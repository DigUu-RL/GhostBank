using GhostBank.Infrastructure.Data.Entities.Audit.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GhostBank.Infrastructure.Data.Mappings.Audit.Identity;

public class UserAuditMap : BaseAuditMap<UserAudit>
{
	public override void Configure(EntityTypeBuilder<UserAudit> builder)
	{
		builder.ToTable(nameof(UserAudit));
		base.Configure(builder);
	}
}
