using GhostBank.Infrastructure.Data.Entities.Identity;

namespace GhostBank.Infrastructure.Repository.Interfaces.Identity;

public interface IUserRepository : IBaseRepository<User>
{
	Task GrantDataBaseAccess(User user);
}
