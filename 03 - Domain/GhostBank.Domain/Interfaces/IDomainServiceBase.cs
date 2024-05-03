using GhostBank.Domain.Models;
using GhostBank.Domain.Requests;

namespace GhostBank.Domain.Interfaces;

public interface IDomainServiceBase<TModel, TRequest> where TRequest : class
{
	Task<TModel> GetByIdAsync(Guid id);
	Task<List<TModel>> GetAllAsync();
	Task<PaginatedListModel<TModel>> GetAsync(SearchRequest<TRequest> request);
	Task<PaginatedListModel<TModel>> GetWithExcludedAsync(SearchRequest<TRequest> request);
	Task CreateAsync(TRequest request);
	Task UpdateAsync(TRequest request);
	Task DeleteAsync(Guid id);
}
