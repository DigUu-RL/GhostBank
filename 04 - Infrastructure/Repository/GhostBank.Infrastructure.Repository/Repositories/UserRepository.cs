using GhostBank.Infrastructure.Data.Contexts;
using GhostBank.Infrastructure.Data.Entities.Identity;
using GhostBank.Infrastructure.Repository.Interfaces;

namespace GhostBank.Infrastructure.Repository.Repositories;

public class UserRepository(GhostBankContext context) : BaseRepository<User>(context), IUserRepository
{

}
