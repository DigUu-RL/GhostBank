using GhostBank.Infrastructure.Data.Entities;
using GhostBank.Infrastructure.Repository.Specifications;

namespace GhostBank.Infrastructure.Repository.Helpers;

public class Search<TEntity> where TEntity : EntityBase
{
	public int Page { get; set; }
	public int Quantity { get; set; }
	public Specification<TEntity>? Specification { get; set; }
}
