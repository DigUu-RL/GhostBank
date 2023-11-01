using GhostBank.Infrastructure.Data.Entities;
using GhostBank.Infrastructure.Repository.Helpers;
using GhostBank.Infrastructure.Repository.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace GhostBank.Infrastructure.Repository.Interfaces;

public interface IBaseRepository<TContext, TEntity> : IUnitOfWork
	where TContext : DbContext
	where TEntity : EntityBase
{
	TContext Context { get; }
	IDbContextTransaction Transaction { get; }
	DbSet<TEntity> Entity { get; }

	Task<TEntity?> GetByIdAsync(Guid id);
	Task<List<TEntity>> GetAllAsync();
	Task<PaginatedList<TEntity>> GetAsync(int page, int quantity, Specification<TEntity>? specification = null);
	Task<PaginatedList<TEntity>> GetWithExcluded(int page, int quantity, Specification<TEntity>? specification = null);
	Task CreateAsync(TEntity entity);
	Task UpdateAsync(TEntity entity);
	Task CreateOrUpdateAsync(TEntity entity);
	Task DeleteAsync(TEntity entity);
}
