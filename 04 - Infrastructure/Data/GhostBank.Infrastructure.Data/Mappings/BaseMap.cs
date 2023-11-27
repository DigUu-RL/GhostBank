using GhostBank.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GhostBank.Infrastructure.Data.Mappings;

public class BaseMap<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : EntityBase
{
	public virtual void Configure(EntityTypeBuilder<TEntity> builder)
	{
		builder.HasKey(x => x.Id);

		builder
			.Property(x => x.CreatedOn)
			.HasColumnType("DATETIME")
			.IsRequired();

		builder
			.Property(x => x.LastUpdate)
			.HasColumnType("DATETIME")
			.IsRequired();

		builder
			.Property(x => x.Excluded)
			.HasDefaultValue(false)
			.IsRequired();
	}
}
