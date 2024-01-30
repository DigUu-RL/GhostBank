using GhostBank.Application.DTOs;

namespace GhostBank.Application.Interface.Authentication;

public interface IApplicationJwtService
{
	Task<UserDTO> ValidateTokenAsync(string? token);
}
