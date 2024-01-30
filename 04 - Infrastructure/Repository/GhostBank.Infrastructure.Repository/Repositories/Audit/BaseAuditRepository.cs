using GhostBank.Infrastructure.Data.Contexts.Audit;
using GhostBank.Infrastructure.Data.Entities.Audit;
using GhostBank.Infrastructure.Repository.Helpers;
using GhostBank.Infrastructure.Repository.Interfaces.Audit;
using GhostBank.Infrastructure.Repository.Specifications;
using GhostBank.Infrastructure.Repository.Specifications.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace GhostBank.Infrastructure.Repository.Repositories.Audit;

public class BaseAuditRepository<TEntity>(GhostBankAuditContext context) : IBaseAuditRepository<TEntity> where TEntity : EntityAuditBase
{
	private readonly GhostBankAuditContext _context = context ?? throw new ArgumentNullException(nameof(context));
	private readonly DbSet<TEntity> _entity = context.Set<TEntity>();
	private readonly IQueryable<TEntity> _query = context.Set<TEntity>().AsQueryable();

	public IQueryable<TEntity> Query(Specification<TEntity>? specification = null)
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

	public virtual async Task CreateAsync(params TEntity[] entities)
	{
		await _entity.AddRangeAsync(entities);
	}

	public virtual Task UpdateAsync(params TEntity[] entities)
	{
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
