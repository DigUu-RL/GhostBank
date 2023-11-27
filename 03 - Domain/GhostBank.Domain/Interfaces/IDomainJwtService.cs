using GhostBank.Infrastructure.Data.Entities.Identity;

namespace GhostBank.Domain.Interfaces;

public interface IDomainJwtService
{
	string GenerateToken(User user);
	void ValidateToken(string token);
}
