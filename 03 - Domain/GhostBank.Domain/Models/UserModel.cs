using GhostBank.Infrastructure.Data.Entities.Identity;
using GhostBank.Infrastructure.Data.Enums;
using GhostBank.Infrastructure.Middleware.Attributes;

namespace GhostBank.Domain.Models;

[Model]
public class UserModel
{
	public Guid Id { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string UserName { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
	public List<UserRole> Roles { get; set; }

    public UserModel()
    {
        
    }

    public UserModel(User entity)
	{
		Id = entity.Id;
		FirstName = entity.FirstName;
		LastName = entity.LastName;
		UserName = entity.UserName;
		Email = entity.Email;
		Password = entity.Password;
		Roles = entity.Roles;
	}
}
