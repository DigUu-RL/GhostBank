using GhostBank.Infrastructure.Data.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GhostBank.Infrastructure.Data.Mappings.Identity;

public class UserMap : BaseMap<User>
{
	public override void Configure(EntityTypeBuilder<User> builder)
	{
		builder.ToTable(nameof(User));

		builder
			.Property(x => x.Id)
			.IsRequired()
			.ValueGeneratedOnAdd();

		builder
			.Property(x => x.Name)
			.HasColumnType("VARCHAR")
			.HasMaxLength(255)
			.IsRequired();

		builder
			.Property(x => x.CPF)
			.HasColumnType("CHAR")
			.HasMaxLength(11)
			.IsRequired();

		builder
			.Property(x => x.CNPJ)
			.HasColumnType("CHAR")
			.HasMaxLength(14)
			.HasDefaultValue(null)
			.IsRequired(false);

		builder
			.Property(x => x.UserName)
			.HasColumnType("VARCHAR")
			.HasMaxLength(255)
			.IsRequired();

		builder
			.Property(x => x.Email)
			.HasColumnType("VARCHAR")
			.HasMaxLength(255)
			.IsRequired();

		builder
			.Property(x => x.Cellphone)
			.HasColumnType("VARCHAR")
			.HasMaxLength(255)
			.IsRequired();

		builder
			.Property(x => x.Password)
			.HasColumnType("VARCHAR")
			.HasMaxLength(255)
			.IsRequired();

		builder
			.Property(x => x.Role)
			.HasColumnType("VARCHAR")
			.HasMaxLength(255)
			.IsRequired();

		builder
			.HasIndex(x => x.UserName)
			.IsUnique();

		builder
			.HasIndex(x => x.Email)
			.IsUnique();

		builder
			.HasMany(x => x.Claims)
			.WithOne(x => x.User)
			.HasForeignKey(x => x.UserId)
			.OnDelete(DeleteBehavior.Cascade);

		builder
			.HasOne(x => x.Address)
			.WithOne(x => x.User)
			.HasForeignKey<Address>(x => x.UserId)
			.OnDelete(DeleteBehavior.Cascade);

		builder
			.HasMany(x => x.Accounts)
			.WithOne(x => x.User)
			.HasForeignKey(x => x.UserId)
			.OnDelete(DeleteBehavior.Cascade);

		base.Configure(builder);
	}
}
