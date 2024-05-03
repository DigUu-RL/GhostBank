using GhostBank.Infrastructure.Data.Contexts;
using GhostBank.Infrastructure.Data.Entities;
using GhostBank.Infrastructure.Repository.Helpers;
using GhostBank.Infrastructure.Repository.Interfaces;
using GhostBank.Infrastructure.Repository.Specifications;
using GhostBank.Infrastructure.Repository.Specifications.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq.Expressions;

namespace GhostBank.Infrastructure.Repository.Repositories;

public class BaseRepository<TEntity>(GhostBankContext context) : IBaseRepository<TEntity> where TEntity : EntityBase
{
	private readonly GhostBankContext _context = context ?? throw new ArgumentNullException(nameof(context));
	private IQueryable<TEntity> _query = context.Set<TEntity>().AsQueryable<TEntity>();

	public void With(params Expression<Func<TEntity, object>>[] properties)
	{
		foreach (Expression<Func<TEntity, object>> item in properties)
			_query = _query.Include(item);
	}

	/// <summary>
	/// Executa uma sequência de comandos SQL no contexto atual
	/// </summary>
	/// <param name="sql">O SQL que será executado</param>
	/// <returns></returns>
	public async Task ExecuteSqlAsync(string sql)
	{
		DatabaseFacade database = _context.Database;
		await database.ExecuteSqlRawAsync(sql);
	}

	public virtual IQueryable<TEntity> Query(Specification<TEntity>? specification = null, bool readOnly = true)
	{
		specification ??= new TrueSpecification<TEntity>();

		_query = _query.Where(specification);
		return readOnly ? _query.AsNoTracking() : _query;
	}

	public virtual async Task<TEntity?> GetByIdAsync(Guid id, bool readOnly = true)
	{
		TEntity? entity = await Query(readOnly: readOnly).SingleOrDefaultAsync(x => x.Id == id && !x.Excluded);
		return entity;
	}

	public virtual async Task<List<TEntity>> GetAllAsync(bool readOnly = true)
	{
		return await Query(readOnly: readOnly).Where(x => !x.Excluded).ToListAsync();
	}

	public virtual async Task<PaginatedList<TEntity>> GetAsync(Search<TEntity> search, bool readOnly = true)
	{
		IQueryable<TEntity> items = Query(search.Specification, readOnly).Where(x => !x.Excluded);
		return await PaginatedList<TEntity>.CreateInstanceAsync(items, search.Page, search.Quantity);
	}

	public virtual async Task<PaginatedList<TEntity>> GetWithExcludedAsync(Search<TEntity> search, bool readOnly = true)
	{
		IQueryable<TEntity> items = Query(search.Specification, readOnly);
		return await PaginatedList<TEntity>.CreateInstanceAsync(items, search.Page, search.Quantity);
	}

	public virtual async Task CreateAsync(params TEntity[] entities)
	{
		foreach (TEntity entity in entities)
		{
			entity.CreatedOn = DateTime.UtcNow;
			entity.LastUpdate = DateTime.UtcNow;
		}

		await _context.AddRangeAsync(entities);
		await _context.SaveChangesAsync();
	}

	public virtual async Task UpdateAsync(params TEntity[] entities)
	{
		foreach (TEntity entity in entities)
			entity.LastUpdate = DateTime.UtcNow;

		_context.UpdateRange(entities);
		await _context.SaveChangesAsync();
	}

	public virtual async Task CreateOrUpdateAsync(params TEntity[] entities)
	{
		TEntity[] createEntities = entities.Where(x => x.Id.Equals(Guid.Empty)).ToArray();
		TEntity[] updateEntities = entities.Where(x => !x.Id.Equals(Guid.Empty)).ToArray();

		if (createEntities.Length != 0)
			await CreateAsync(createEntities);

		if (updateEntities.Length != 0)
			await UpdateAsync(updateEntities);
	}

	public virtual async Task DeleteAsync(params TEntity[] entities)
	{
		_context.RemoveRange(entities);
		await _context.SaveChangesAsync();
	}

	public virtual async Task CommitAsync()
	{
		await _context.Transaction.CommitAsync();
	}

	public virtual async Task RollbackAsync()
	{
		await _context.Transaction.RollbackAsync();
	}
}
