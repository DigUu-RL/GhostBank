using GhostBank.Application.DTOs;
using GhostBank.Domain.Helpers;
using GhostBank.Infrastructure.Data.Entities.Identity;

namespace GhostBank.Application.Interface;

public interface IApplicationServiceBase<TDTO, TRequest> where TRequest : class
{
	Task<TDTO> GetByIdAsync(Guid id);
	Task<List<TDTO>> GetAllAsync();
	Task<PaginatedListDTO<TDTO>> GetAsync(Search<TRequest> search);
	Task<PaginatedListDTO<TDTO>> GetWithExcludedAsync(Search<TRequest> search);
	Task CreateAsync(TRequest request);
	Task UpdateAsync(TRequest request);
	Task DeleteAsync(Guid id);
}
