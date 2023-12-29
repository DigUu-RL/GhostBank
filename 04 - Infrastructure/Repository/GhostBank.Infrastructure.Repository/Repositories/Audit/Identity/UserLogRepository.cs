using GhostBank.Infrastructure.Data.Contexts.Audit;
using GhostBank.Infrastructure.Data.Entities.Audit.Identity;
using GhostBank.Infrastructure.Repository.Interfaces.Audit.Identity;

namespace GhostBank.Infrastructure.Repository.Repositories.Audit.Identity;

public class UserLogRepository(GhostBankAuditContext context) : BaseLogRepository<UserAudit>(context), IUserLogRepository
{
}
