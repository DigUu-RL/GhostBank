using GhostBank.Infrastructure.Data.Entities.Identity;

namespace GhostBank.Infrastructure.Repository.Interfaces;

public interface IRepositoryWrapper
{
    IBaseRepository<User> User { get; }
    IBaseRepository<UserClaim> UserClaim { get; }
}
