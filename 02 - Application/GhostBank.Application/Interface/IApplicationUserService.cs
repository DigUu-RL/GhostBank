using GhostBank.Application.DTOs;
using GhostBank.Domain.Requests;

namespace GhostBank.Application.Interface;

public interface IApplicationUserService : IApplicationServiceBase<UserDTO, UserRequest>
{
}
