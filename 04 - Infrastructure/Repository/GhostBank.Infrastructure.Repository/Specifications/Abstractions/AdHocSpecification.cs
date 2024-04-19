using System.Linq.Expressions;

namespace GhostBank.Infrastructure.Repository.Specifications.Abstractions;

public class AdHocSpecification<T>(Expression<Func<T, bool>> expression) : Specification<T>
{
	private readonly Expression<Func<T, bool>> _expression = expression ?? throw new ArgumentNullException(nameof(expression));

	public override Expression<Func<T, bool>> ToExpression()
	{
		return _expression;
	}
}
