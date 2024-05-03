using GhostBank.Application.DTOs;
using GhostBank.Domain.Requests;

namespace GhostBank.Application.Interface;

public interface IApplicationServiceBase<TDTO, TRequest> where TRequest : class
{
	Task<TDTO> GetByIdAsync(Guid id);
	Task<List<TDTO>> GetAllAsync();
	Task<PaginatedListDTO<TDTO>> GetAsync(SearchRequest<TRequest> search);
	Task<PaginatedListDTO<TDTO>> GetWithExcludedAsync(SearchRequest<TRequest> search);
	Task CreateAsync(TRequest request);
	Task UpdateAsync(TRequest request);
	Task DeleteAsync(Guid id);
}
