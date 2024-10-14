using System.Linq.Expressions;

namespace RulesEngine;

public class SymboticEligibleSpecification : Specification<CpiSkuDimensionVariant>
{
    public override Expression<Func<CpiSkuDimensionVariant, bool>> ToExpression()
    {
        return variant => variant.IsSymboticEligible;
    }
}