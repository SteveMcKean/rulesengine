using System.Linq.Expressions;

namespace RulesEngine;

public class AllowedNotRecommendedTippingSpecification : Specification<CpiSkuDimensionVariant>
{
    public override Expression<Func<CpiSkuDimensionVariant, bool>> ToExpression()
    {
        return variant => variant.AspectRatio > CpiSkuDimensionVariant.MinAspectRatio &&
                          variant.AspectRatio <= CpiSkuDimensionVariant.MaxAspectRatio;
    }
}