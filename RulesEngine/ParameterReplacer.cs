using System.Linq.Expressions;

namespace RulesEngine;

public class ParameterReplacer : ExpressionVisitor
{
    private readonly ParameterExpression parameter;

    protected override Expression VisitParameter(ParameterExpression node)
    {
        return parameter;
    }

    internal ParameterReplacer(ParameterExpression parameter)
    {
        this.parameter = parameter;
    }
}