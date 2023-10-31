using System.Linq.Expressions;

namespace GhostBank.Infrastructure.Repository.Specifications.Abstractions;

public class FalseSpecification<T> : Specification<T>
{
	public override Expression<Func<T, bool>> ToExpression()
	{
		return (T x) => false;
	}
}
