using GhostBank.Infrastructure.Data.Contexts;
using GhostBank.Infrastructure.Data.Entities;
using GhostBank.Infrastructure.Repository.Interfaces;
using GhostBank.Infrastructure.Repository.Specifications;
using Microsoft.EntityFrameworkCore;

namespace GhostBank.Infrastructure.Repository.Repositories;

public class BaseRepository<TEntity>(GhostBankContext context) : IBaseRepository<TEntity> where TEntity : EntityBase
{
	public DbContext Context { get; } = context ?? throw new ArgumentNullException(nameof(context));

	public Task CommitAsync()
	{
		throw new NotImplementedException();
	}

	public Task CreateAsync(TEntity entity)
	{
		throw new NotImplementedException();
	}

	public Task CreateOrUpdateAsync(TEntity entity)
	{
		throw new NotImplementedException();
	}

	public Task DeleteAsync(TEntity entity)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<TEntity>> GetAllAsync()
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<TEntity>> GetAsync(Specification<TEntity>? specification = null)
	{
		throw new NotImplementedException();
	}

	public Task<TEntity> GetByIdAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public Task RollbackAsync()
	{
		throw new NotImplementedException();
	}

	public Task UpdateAsync(TEntity entity)
	{
		throw new NotImplementedException();
	}
}
