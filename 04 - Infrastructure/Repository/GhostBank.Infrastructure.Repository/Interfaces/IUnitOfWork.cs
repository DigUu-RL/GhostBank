namespace GhostBank.Infrastructure.Repository.Interfaces;

public interface IUnitOfWork
{
	Task CommitAsync();
	Task RollbackAsync();
}
