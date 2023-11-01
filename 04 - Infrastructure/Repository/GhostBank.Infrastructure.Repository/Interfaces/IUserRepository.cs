using GhostBank.Infrastructure.Data.Contexts;
using GhostBank.Infrastructure.Data.Entities.Identity;

namespace GhostBank.Infrastructure.Repository.Interfaces;

public interface IUserRepository : IBaseRepository<GhostBankContext, User>
{
}
