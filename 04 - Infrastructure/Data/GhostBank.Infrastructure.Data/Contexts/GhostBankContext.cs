using GhostBank.Infrastructure.Data.Mappings.Bank;
using GhostBank.Infrastructure.Data.Mappings.Cards;
using GhostBank.Infrastructure.Data.Mappings.Identity;
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
		#region IDENTITY

		modelBuilder.ApplyConfiguration(new UserMap());
		modelBuilder.ApplyConfiguration(new UserClaimMap());
		modelBuilder.ApplyConfiguration(new AddressMap());

		#endregion

		#region BANK

		modelBuilder.ApplyConfiguration(new AccountMap());
		modelBuilder.ApplyConfiguration(new PixMap());

		#endregion

		#region CARDS

		modelBuilder.ApplyConfiguration(new CardMap());
		modelBuilder.ApplyConfiguration(new CreditCardMap());
		modelBuilder.ApplyConfiguration(new DebitCardMap());
		modelBuilder.ApplyConfiguration(new VirtualCardMap());
		modelBuilder.ApplyConfiguration(new InvoiceMap());

		#endregion

		base.OnModelCreating(modelBuilder);
	}
}
