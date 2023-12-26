using GhostBank.Domain.Helpers;
using GhostBank.Domain.Models;

namespace GhostBank.Domain.Interfaces;

public interface IDomainServiceBase<TModel, TRequest> where TRequest : class
{
	Task<TModel> GetByIdAsync(Guid id);
	Task<List<TModel>> GetAllAsync();
	Task<PaginatedListModel<TModel>> GetAsync(Search<TRequest> search);
	Task<PaginatedListModel<TModel>> GetWithExcludedAsync(Search<TRequest> search);
	Task CreateAsync(TRequest request);
	Task UpdateAsync(TRequest request);
	Task DeleteAsync(Guid id);
}
