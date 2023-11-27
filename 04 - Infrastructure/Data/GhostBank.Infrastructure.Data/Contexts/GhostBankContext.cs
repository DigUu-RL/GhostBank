using GhostBank.Infrastructure.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GhostBank.Infrastructure.Data.Contexts;

public class GhostBankContext : DbContext
{
	private readonly IConfiguration _configuration;

    public GhostBankContext()
    {
        
    }

    public GhostBankContext(IConfiguration configuration)
	{
		_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
	}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		string connectionString = _configuration.GetConnectionString(nameof(GhostBank))!;

		optionsBuilder.UseSqlServer(connectionString);
		base.OnConfiguring(optionsBuilder);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new UserMap());

		base.OnModelCreating(modelBuilder);
	}
}
