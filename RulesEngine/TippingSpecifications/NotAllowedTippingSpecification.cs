using System.Linq.Expressions;

namespace RulesEngine;

public class NotAllowedTippingSpecification : Specification<CpiSkuDimensionVariant>
{
    public override Expression<Func<CpiSkuDimensionVariant, bool>> ToExpression()
    {
        // is the ration < 1.0
        return variant => variant.AspectRatio < CpiSkuDimensionVariant.MinAspectRatio;
    }
}