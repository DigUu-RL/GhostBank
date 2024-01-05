using GhostBank.Domain.Models.Identity;
using GhostBank.Domain.Requests.Identity;

namespace GhostBank.Domain.Interfaces.Identity;

public interface IDomainUserService : IDomainServiceBase<UserModel, UserRequest>
{

}
