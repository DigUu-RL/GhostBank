using GhostBank.Infrastructure.Data.Entities.Audit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GhostBank.Infrastructure.Data.Mappings.Audit;

public class BaseAuditMap<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : EntityAuditBase
{
	public virtual void Configure(EntityTypeBuilder<TEntity> builder)
	{
		builder.HasKey(x => x.Id);

		builder
			.Property(x => x.Date)
			.HasColumnType("DATETIME")
			.IsRequired();

		builder
			.Property(x => x.Column)
			.HasColumnType("VARCHAR")
			.HasMaxLength(255)
			.IsRequired(false);

		builder
			.Property(x => x.OldValue)
			.HasColumnType("VARCHAR")
			.HasMaxLength(255)
			.IsRequired(false);

		builder
			.Property(x => x.NewValue)
			.HasColumnType("VARCHAR")
			.HasMaxLength(255)
			.IsRequired(false);

		builder
			.Property(x => x.Action)
			.HasColumnType("VARCHAR")
			.HasMaxLength(255)
			.IsRequired();

		builder
			.Property(x => x.Action)
			.HasColumnType("VARCHAR")
			.HasMaxLength(255)
			.IsRequired();
	}
}
