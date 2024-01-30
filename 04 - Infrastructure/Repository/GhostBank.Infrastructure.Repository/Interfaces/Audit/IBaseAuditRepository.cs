using GhostBank.Infrastructure.Data.Entities.Audit;
using GhostBank.Infrastructure.Repository.Specifications;

namespace GhostBank.Infrastructure.Repository.Interfaces.Audit;

public interface IBaseAuditRepository<TEntity> where TEntity : EntityAuditBase
{
	IQueryable<TEntity> Query(Specification<TEntity>? specification = null);
	Task<List<TEntity>> GetAllAsync();
	Task<TEntity?> GetByIdAsync(Guid id);
	Task CreateAsync(params TEntity[] entities);
	Task UpdateAsync(params TEntity[] entities);
	Task CreateOrUpdateAsync(TEntity entity);
	Task DeleteAsync(TEntity entity);
}
