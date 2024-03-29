﻿using GhostBank.Infrastructure.Data.Entities.Audit.Identity;

namespace GhostBank.Infrastructure.Repository.Interfaces.Audit.Identity;

public interface IUserAuditRepository : IBaseAuditRepository<UserAudit>
{
}
