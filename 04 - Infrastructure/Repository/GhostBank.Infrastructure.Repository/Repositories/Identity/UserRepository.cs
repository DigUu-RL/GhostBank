using GhostBank.Infrastructure.Data.Contexts;
using GhostBank.Infrastructure.Data.Entities.Identity;
using GhostBank.Infrastructure.Repository.Interfaces.Identity;

namespace GhostBank.Infrastructure.Repository.Repositories.Identity;

public class UserRepository(GhostBankContext context) : BaseRepository<User>(context), IUserRepository
{
	public async Task GrantDataBaseAccess(User user)
	{
		string sql = $@"
            IF NOT EXISTS (SELECT [name]  FROM sys.sql_logins WHERE name LIKE '%{user.Id}%')
                BEGIN
                    CREATE LOGIN [{user.Id}.{user.UserName}] WITH PASSWORD=N'{user.Password}';
                END
            ELSE
                BEGIN
                    ALTER LOGIN [{user.Id}.{user.UserName}] WITH PASSWORD=N'{user.Password}';
                END

            IF NOT EXISTS (SELECT * FROM sys.sysusers WHERE name LIKE '%{user.Id}%')
                BEGIN
                    CREATE USER [{user.Id}.{user.UserName}] FOR LOGIN [{user.Id}.{user.UserName}]
                END

            ALTER USER [{user.Id}.{user.UserName}] WITH DEFAULT_SCHEMA=[dbo]
            ALTER ROLE [db_datareader] ADD MEMBER [{user.Id}.{user.UserName}]
            ALTER ROLE [db_datawriter] ADD MEMBER [{user.Id}.{user.UserName}]
        ";

		await ExecuteSqlAsync(sql);
	}
}
