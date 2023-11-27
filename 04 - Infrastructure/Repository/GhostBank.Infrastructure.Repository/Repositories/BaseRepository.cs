using GhostBank.Infrastructure.Data.Contexts;
using GhostBank.Infrastructure.Data.Entities;
using GhostBank.Infrastructure.Repository.Helpers;
using GhostBank.Infrastructure.Repository.Interfaces;
using GhostBank.Infrastructure.Repository.Specifications;
using GhostBank.Infrastructure.Repository.Specifications.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;
using System.Security.Cryptography;

namespace GhostBank.Infrastructure.Repository.Repositories;

public class BaseRepository<TEntity>(GhostBankContext context) : IBaseRepository<TEntity> where TEntity : EntityBase
{
	private readonly GhostBankContext _context = context;
	private readonly DbSet<TEntity> _entity = context.Set<TEntity>();
	private IQueryable<TEntity> _query = context.Set<TEntity>().AsQueryable();

    public virtual void Include<TProperty>(Expression<Func<TEntity, TProperty>> expression)
	{
		_query = _query.Include(expression);
	}

	public virtual IQueryable<TEntity> Query(Specification<TEntity>? specification = null)
	{
		specification ??= new TrueSpecification<TEntity>();
		return _query.Where(specification);
	}

	public virtual async Task<TEntity?> GetByIdAsync(Guid id)
	{
		TEntity? entity = await _query.SingleOrDefaultAsync(x => x.Id == id);
		return entity;
	}

	public virtual async Task<List<TEntity>> GetAllAsync()
	{
		return await _query.ToListAsync();
	}

	public virtual async Task<PaginatedList<TEntity>> GetAsync(int page, int quantity, Specification<TEntity>? specification = null)
	{
		specification ??= new TrueSpecification<TEntity>();

		IQueryable<TEntity> items = _query.Where(specification);
		return await PaginatedList<TEntity>.CreateInstanceAsync(items, page, quantity);
	}

	public virtual async Task<PaginatedList<TEntity>> GetWithExcludedAsync(int page, int quantity, Specification<TEntity>? specification = null)
	{
		specification ??= new TrueSpecification<TEntity>();
		specification &= new ExpressionSpecification<TEntity>(x => !x.Excluded);

		IQueryable<TEntity> items = _query.Where(specification);
		return await PaginatedList<TEntity>.CreateInstanceAsync(items, page, quantity);
	}

	public virtual async Task CreateAsync(params TEntity[] entities)
	{
		foreach (TEntity entity in entities)
		{
			entity.CreatedOn = DateTime.UtcNow;
			entity.LastUpdate = DateTime.UtcNow;
		}

		await _entity.AddRangeAsync(entities);
	}

	public virtual Task UpdateAsync(params TEntity[] entities)
	{
		foreach (TEntity entity in entities)
			entity.LastUpdate = DateTime.UtcNow;

		_entity.UpdateRange(entities);
		return Task.CompletedTask;
	}

	public virtual async Task CreateOrUpdateAsync(TEntity entity)
	{
		Task task = entity.Id.Equals(Guid.Empty) ? CreateAsync(entity) : UpdateAsync(entity);
		await task;
	}

	public virtual Task DeleteAsync(TEntity entity)
	{
		_entity.Remove(entity);
		return Task.CompletedTask;
	}

	public virtual async Task CommitAsync()
	{
		await _context.SaveChangesAsync();
	}

	public virtual Task RollbackAsync()
	{
		return Task.CompletedTask;
	}
}
