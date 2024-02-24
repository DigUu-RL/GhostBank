# Run this code on Package Manager Console to create a lasted migration:
	Add-Migration -Project GhostBank.Infrastructure.Data -StartUpProject GhostBank.Infrastructure.Data -Context GhostBankContext

# Run this code on Package Manager Console to update your local Database:
	Update-Database -Verbose -Connection "Server=localhost\SQLEXPRESS; Database=GhostBank; User Id=root; Password=Veneno13$; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False;" -Project GhostBank.Infrastructure.Data -StartUpProject GhostBank.Infrastructure.Data -Context GhostBankContext

# Audit
	# Run this code on Package Manager Console to create a lasted migration:
		Add-Migration -Project GhostBank.Infrastructure.Data -StartUpProject GhostBank.Infrastructure.Data -Context GhostBankAuditContext

	# Run this code on Package Manager Console to update your local Database:
		Update-Database -Verbose -Connection "Server=localhost\SQLEXPRESS; Database=GhostBankAudit; User Id=root; Password=Veneno13$; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False;" -Project GhostBank.Infrastructure.Data -StartUpProject GhostBank.Infrastructure.Data -Context GhostBankAuditContext