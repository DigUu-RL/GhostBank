﻿using GhostBank.Domain.Attributes;
using GhostBank.Infrastructure.Data.Entities.Identity;
using GhostBank.Infrastructure.Data.Enums.Identity;

namespace GhostBank.Domain.Models.Identity;

[Model]
public class UserModel
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string UserName { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
	public UserRole Role { get; set; }

	public UserModel()
	{

	}

	public UserModel(User entity)
	{
		Id = entity.Id;
		Name = entity.Name;
		UserName = entity.UserName;
		Email = entity.Email;
		Password = entity.Password;
		Role = entity.Role;
	}
}
