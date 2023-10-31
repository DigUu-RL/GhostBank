using System.Linq.Expressions;

namespace GhostBank.Infrastructure.Repository.Specifications.Abstractions;

public class NotSpecification<T>(Specification<T> specification) : Specification<T>
{
	private readonly Specification<T> _specification = specification ?? throw new ArgumentNullException(nameof(specification));

	public override Expression<Func<T, bool>> ToExpression()
	{
		Expression<Func<T, bool>> expression = _specification.ToExpression();
		return Expression.Lambda<Func<T, bool>>(Expression.Not(expression.Body), expression.Parameters);
	}
}
