using System.Linq.Expressions;

namespace RulesEngine;

public class NotSpecification<T> : Specification<T>
{
    private readonly Specification<T> spec;

    public NotSpecification(Specification<T> spec)
    {
        this.spec = spec;
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        var expression = spec.ToExpression();

        var parameter = Expression.Parameter(typeof(T));

        var negated = Expression.Not(expression.Body);
        negated = (UnaryExpression)new ParameterReplacer(parameter).Visit(negated);

        return Expression.Lambda<Func<T, bool>>(negated, parameter);
    }
}