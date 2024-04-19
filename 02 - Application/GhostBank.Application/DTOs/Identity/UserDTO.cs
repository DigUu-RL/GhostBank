using GhostBank.Domain.Attributes;
using GhostBank.Domain.Models.Identity;
using GhostBank.Infrastructure.Data.Enums.Identity;

namespace GhostBank.Application.DTOs;

[DTO]
public class UserDTO
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string UserName { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
	public UserRole Role { get; set; }

	public UserDTO()
	{

	}

	public UserDTO(UserModel model)
	{
		Id = model.Id;
		Name = model.Name;
		UserName = model.UserName;
		Email = model.Email;
		Password = model.Password;
		Role = model.Role;
	}
}
