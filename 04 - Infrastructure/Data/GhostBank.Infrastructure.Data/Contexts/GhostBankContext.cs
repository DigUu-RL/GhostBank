using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GhostBank.Infrastructure.Data.Contexts;

public class GhostBankContext(IConfiguration configuration) : DbContext
{
	private readonly IConfiguration _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
	}
}
