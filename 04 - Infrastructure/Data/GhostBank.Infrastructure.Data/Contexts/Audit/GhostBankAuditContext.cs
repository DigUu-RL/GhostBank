using GhostBank.Infrastructure.Data.Mappings.Audit.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GhostBank.Infrastructure.Data.Contexts.Audit;

public class GhostBankAuditContext : DbContext
{
    private readonly IConfiguration _configuration;

    public GhostBankAuditContext()
    {

    }

	public GhostBankAuditContext(IConfiguration configuration)
	{
		_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		string connectionString = _configuration.GetConnectionString($"{nameof(GhostBank)}{nameof(Audit)}")!;

		optionsBuilder.UseSqlServer(connectionString);
		base.OnConfiguring(optionsBuilder);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		#region IDENTITY

		modelBuilder.ApplyConfiguration(new UserAuditMap());

		#endregion

		base.OnModelCreating(modelBuilder);
	}
}
