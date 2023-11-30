using GhostBank.Infrastructure.Data.Entities.Identity;

namespace GhostBank.Domain.Interfaces;

public interface IDomainJwtService
{
	string GenerateToken(User user);
	Task<User> ValidateTokenAsync(string token);
}
