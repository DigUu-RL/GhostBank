using GhostBank.Infrastructure.Data.Entities;
using GhostBank.Infrastructure.Repository.Helpers;
using GhostBank.Infrastructure.Repository.Specifications;
using System.Linq.Expressions;

namespace GhostBank.Infrastructure.Repository.Interfaces;

public interface IBaseRepository<TEntity> : IUnitOfWork where TEntity : EntityBase
{
	void With(params Expression<Func<TEntity, object>>[] properties);
	Task ExecuteSQLAsync(string sql);
	IQueryable<TEntity> Query(Specification<TEntity>? specification = null, bool readOnly = true);
	Task<TEntity?> GetByIdAsync(Guid id, bool readOnly = true);
	Task<List<TEntity>> GetAllAsync(bool readOnly = true);
	Task<PaginatedList<TEntity>> GetAsync(int page, int quantity, Specification<TEntity>? specification = null, bool readOnly = true);

	Task<PaginatedList<TEntity>> GetWithExcludedAsync(
		int page,
		int quantity,
		Specification<TEntity>? specification = null,
		bool readOnly = true
	);

	Task CreateAsync(params TEntity[] entities);
	Task UpdateAsync(params TEntity[] entities);
	Task CreateOrUpdateAsync(params TEntity[] entities);
	Task DeleteAsync(params TEntity[] entities);
}
