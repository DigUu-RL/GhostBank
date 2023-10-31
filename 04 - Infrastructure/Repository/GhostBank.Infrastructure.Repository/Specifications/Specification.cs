using GhostBank.Infrastructure.Repository.Specifications.Abstractions;
using System.Linq.Expressions;

namespace GhostBank.Infrastructure.Repository.Specifications;

public abstract class Specification<T>
{
	public static implicit operator Expression<Func<T, bool>>(Specification<T> specification) => specification.ToExpression();
	public static implicit operator Func<T, bool>(Specification<T> specification) => specification.ToExpression().Compile();

	public static Specification<T> operator &(Specification<T> left, Specification<T> right)
	{
		return new AndSpecification<T>(left, right);
	}

	public static Specification<T> operator |(Specification<T> left, Specification<T> right)
	{
		return new OrSpecification<T>(left, right);
	}

	public static Specification<T> operator !(Specification<T> specification)
	{
		return new NotSpecification<T>(specification);
	}

	public abstract Expression<Func<T, bool>> ToExpression();
}
