using GhostBank.Infrastructure.Data.Contexts;
using GhostBank.Infrastructure.Data.Entities;
using GhostBank.Infrastructure.Repository.Helpers;
using GhostBank.Infrastructure.Repository.Interfaces;
using GhostBank.Infrastructure.Repository.Specifications;
using GhostBank.Infrastructure.Repository.Specifications.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace GhostBank.Infrastructure.Repository.Repositories;

public class BaseRepository<TEntity>(GhostBankContext context) : IBaseRepository<GhostBankContext, TEntity> where TEntity : EntityBase
{
	public GhostBankContext Context { get; } = context;
	public IDbContextTransaction Transaction { get; } = context.Database.BeginTransaction();
	public DbSet<TEntity> Entity { get; } = context.Set<TEntity>();

	public virtual async Task<TEntity?> GetByIdAsync(Guid id)
	{
		TEntity? entity = await Context.FindAsync<TEntity>(id);
		return entity;
	}

	public virtual async Task<List<TEntity>> GetAllAsync()
	{
		return await Entity.ToListAsync();
	}

	public virtual async Task<PaginatedList<TEntity>> GetAsync(int page, int quantity, Specification<TEntity>? specification = null)
	{
		specification ??= new TrueSpecification<TEntity>();

		IQueryable<TEntity> items = Entity.Where(specification);
		return await PaginatedList<TEntity>.CreateInstanceAsync(items, page, quantity);
	}

	public virtual async Task<PaginatedList<TEntity>> GetWithExcludedAsync(int page, int quantity, Specification<TEntity>? specification = null)
	{
		specification ??= new TrueSpecification<TEntity>();
		specification &= new ExpressionSpecification<TEntity>(x => !x.Excluded);

		IQueryable<TEntity> items = Entity.Where(specification);
		return await PaginatedList<TEntity>.CreateInstanceAsync(items, page, quantity);
	}

	public virtual async Task CreateAsync(TEntity entity)
	{
		await Entity.AddAsync(entity);
	}

	public virtual Task UpdateAsync(TEntity entity)
	{
		Entity.Update(entity);
		return Task.CompletedTask;
	}

	public virtual async Task CreateOrUpdateAsync(TEntity entity)
	{
		Task task = entity.Id.Equals(Guid.Empty) ? CreateAsync(entity) : UpdateAsync(entity);
		await task;
	}

	public virtual Task DeleteAsync(TEntity entity)
	{
		Entity.Remove(entity);
		return Task.CompletedTask;
	}

	public virtual async Task CommitAsync()
	{
		await Context.SaveChangesAsync();
		await Transaction.CommitAsync();
	}

	public virtual async Task RollbackAsync()
	{
		await Transaction.RollbackAsync();
	}
}
