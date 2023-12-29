using GhostBank.Infrastructure.Data.Entities;
using GhostBank.Infrastructure.Repository.Helpers;
using GhostBank.Infrastructure.Repository.Specifications;

namespace GhostBank.Infrastructure.Repository.Interfaces;

public interface IBaseRepository<TEntity> : IUnitOfWork where TEntity : EntityBase
{
	void With(Func<IQueryable<TEntity>, IQueryable<TEntity>> expression);
	Task RunSqlAsync(string sql);
	IQueryable<TEntity> Query(Specification<TEntity>? specification = null);
	Task<TEntity?> GetByIdAsync(Guid id);
	Task<List<TEntity>> GetAllAsync();
	Task<PaginatedList<TEntity>> GetAsync(int page, int quantity, Specification<TEntity>? specification = null);
	Task<PaginatedList<TEntity>> GetWithExcludedAsync(int page, int quantity, Specification<TEntity>? specification = null);
	Task CreateAsync(params TEntity[] entities);
	Task UpdateAsync(params TEntity[] entities);
	Task CreateOrUpdateAsync(TEntity entity);
	Task DeleteAsync(TEntity entity);
}
