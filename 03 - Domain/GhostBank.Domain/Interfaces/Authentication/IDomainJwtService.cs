using GhostBank.Domain.Models.Authentication;
using GhostBank.Infrastructure.Data.Entities.Identity;

namespace GhostBank.Domain.Interfaces.Authentication;

public interface IDomainJwtService
{
	AccessTokenModel GenerateToken(User user);
	Task<User> ValidateTokenAsync(string token);
}
