using GhostBank.Domain.Models;
using GhostBank.Infrastructure.Data.Enums;
using GhostBank.Infrastructure.Middleware.Attributes;

namespace GhostBank.Application.DTOs;

[DTO]
public class UserDTO
{
	public Guid Id { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string UserName { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
	public List<UserRole> Roles { get; set; }

	public UserDTO()
	{

	}

	public UserDTO(UserModel model)
	{
		Id = model.Id;
		FirstName = model.FirstName;
		LastName = model.LastName;
		UserName = model.UserName;
		Email = model.Email;
		Password = model.Password;
		Roles = model.Roles;
	}
}
