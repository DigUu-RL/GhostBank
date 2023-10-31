using GhostBank.Infrastructure.Data.Entities;
using GhostBank.Infrastructure.Repository.Specifications;
using Microsoft.EntityFrameworkCore;

namespace GhostBank.Infrastructure.Repository.Interfaces;

public interface IBaseRepository<TEntity> : IUnitOfWork where TEntity : EntityBase
{
    DbContext Context { get; }

    Task<TEntity> GetByIdAsync(Guid id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> GetAsync(Specification<TEntity>? specification = null);
    Task CreateAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task CreateOrUpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}
