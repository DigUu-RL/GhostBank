using GhostBank.Infrastructure.Data.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GhostBank.Infrastructure.Data.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.ToTable(nameof(User));
		builder.HasKey(x => x.Id);

		builder
			.HasIndex(x => x.Email)
			.IsUnique();

		builder
			.Property(x => x.Id)
			.HasColumnType("GUID")
			.IsRequired()
			.ValueGeneratedOnAdd();

		builder
			.Property(x => x.FirstName)
			.HasColumnType("VARCHAR")
			.HasMaxLength(255)
			.IsRequired();

		builder
			.Property(x => x.LastName)
			.HasColumnType("VARCHAR")
			.HasMaxLength(255)
			.IsRequired();

		builder
			.Property(x => x.Email)
			.HasColumnType("VARCHAR")
			.HasMaxLength(255)
			.IsRequired();

		builder
			.Property(x => x.Password)
			.HasColumnType("VARCHAR")
			.HasMaxLength(255)
			.IsRequired();
	}
}
