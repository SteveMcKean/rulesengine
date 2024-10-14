using System.Linq.Expressions;

namespace RulesEngine;

public class OrSpecification<T> : Specification<T>
{
    private readonly Specification<T> left;
    private readonly Specification<T> right;

    public OrSpecification(Specification<T> left, Specification<T> right)
    {
        this.left = left;
        this.right = right;
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        var leftExpression = left.ToExpression();
        var rightExpression = right.ToExpression();

        var parameter = Expression.Parameter(typeof(T));

        var combined = Expression.OrElse(leftExpression.Body, rightExpression.Body);
        combined = (BinaryExpression)new ParameterReplacer(parameter).Visit(combined);

        return Expression.Lambda<Func<T, bool>>(combined, parameter);
    }
}