using GhostBank.Application.DTOs;
using GhostBank.Domain.Requests.Identity;

namespace GhostBank.Application.Interface.Identity;

public interface IApplicationUserService : IApplicationServiceBase<UserDTO, UserRequest>
{
}
