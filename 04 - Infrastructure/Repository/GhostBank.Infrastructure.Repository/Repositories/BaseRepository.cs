using GhostBank.Infrastructure.Data.Contexts;
using GhostBank.Infrastructure.Data.Entities;
using GhostBank.Infrastructure.Repository.Helpers;
using GhostBank.Infrastructure.Repository.Interfaces;
using GhostBank.Infrastructure.Repository.Specifications;
using GhostBank.Infrastructure.Repository.Specifications.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace GhostBank.Infrastructure.Repository.Repositories;

public class BaseRepository<TEntity>(GhostBankContext context) : IBaseRepository<TEntity> where TEntity : EntityBase
{
	private readonly GhostBankContext _context = context ?? throw new ArgumentNullException(nameof(context));
	private readonly IDbContextTransaction _transaction = context.Transaction;
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
	public async Task ExecuteSQLAsync(string sql)
	{
		DatabaseFacade database = _context.Database;
		await database.ExecuteSqlRawAsync(sql);
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="specification"></param>
	/// <param name="readOnly"></param>
	/// <returns></returns>
	public virtual IQueryable<TEntity> Query(Specification<TEntity>? specification = null, bool readOnly = true)
	{
		specification ??= new TrueSpecification<TEntity>();
		specification &= new AdHocSpecification<TEntity>(x => !x.Excluded);

		_query = _query.Where(specification);

		return readOnly ? _query.AsNoTracking() : _query;
	}

	public virtual async Task<TEntity?> GetByIdAsync(Guid id, bool readOnly = true)
	{
		TEntity? entity = await Query(readOnly: readOnly).SingleOrDefaultAsync(x => x.Id == id);
		return entity;
	}

	public virtual async Task<List<TEntity>> GetAllAsync(bool readOnly = true)
	{
		return await Query(readOnly: readOnly).ToListAsync();
	}

	public virtual async Task<PaginatedList<TEntity>> GetAsync(
		int page,
		int quantity,
		Specification<TEntity>? specification = null,
		bool readOnly = true
	)
	{
		IQueryable<TEntity> items = Query(specification, readOnly);
		return await PaginatedList<TEntity>.CreateInstanceAsync(items, page, quantity);
	}

	public virtual async Task<PaginatedList<TEntity>> GetWithExcludedAsync(
		int page,
		int quantity,
		Specification<TEntity>? specification = null,
		bool readOnly = true
	)
	{
		specification ??= new TrueSpecification<TEntity>();
		specification &= new AdHocSpecification<TEntity>(x => !x.Excluded);

		IQueryable<TEntity> items = Query(specification, readOnly);
		return await PaginatedList<TEntity>.CreateInstanceAsync(items, page, quantity);
	}

	public virtual async Task CreateAsync(params TEntity[] entities)
	{
		foreach (TEntity entity in entities)
		{
			entity.CreatedOn = DateTime.UtcNow;
			entity.LastUpdate = DateTime.UtcNow;
		}

		await _context.AddRangeAsync(entities);
	}

	public virtual Task UpdateAsync(params TEntity[] entities)
	{
		foreach (TEntity entity in entities)
			entity.LastUpdate = DateTime.UtcNow;

		_context.UpdateRange(entities);
		return Task.CompletedTask;
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

	public virtual Task DeleteAsync(params TEntity[] entities)
	{
		_context.RemoveRange(entities);
		return Task.CompletedTask;
	}

	public virtual async Task CommitAsync()
	{
		await _transaction.CommitAsync();
	}

	public virtual async Task RollbackAsync()
	{
		await _transaction.RollbackAsync();
	}
}
