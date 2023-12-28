using GhostBank.Infrastructure.Data.Entities.Audit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GhostBank.Infrastructure.Data.Mappings.Audit;

public class BaseLogMap<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : EntityLogBase
{
	public void Configure(EntityTypeBuilder<TEntity> builder)
	{
		
	}
}
