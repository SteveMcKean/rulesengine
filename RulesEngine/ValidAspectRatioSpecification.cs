using System.Linq.Expressions;

namespace RulesEngine;

public class ValidAspectRatioSpecification : Specification<CpiSkuDimensionVariant>
{
    public override Expression<Func<CpiSkuDimensionVariant, bool>> ToExpression()
    {
        return variant => variant.AspectRatio <= CpiSkuDimensionVariant.MaxAspectRatio;
    }
}