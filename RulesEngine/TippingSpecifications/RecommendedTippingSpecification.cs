using System.Linq.Expressions;

namespace RulesEngine;

public class RecommendedTippingSpecification : Specification<CpiSkuDimensionVariant>
{
    public override Expression<Func<CpiSkuDimensionVariant, bool>> ToExpression()
    {
        return variant => variant.AspectRatio > CpiSkuDimensionVariant.MaxAspectRatio &&
                          variant.AspectRatio <= CpiSkuDimensionVariant.ForcedAspectRatio;
    }
}