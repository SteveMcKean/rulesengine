using System.Linq.Expressions;

namespace RulesEngine;

public class ValidAspectRatioSpecification : Specification<CpiSkuDimensionVariant>
{
    public override Expression<Func<CpiSkuDimensionVariant, bool>> ToExpression()
    {
        return variant => variant.AspectRatio <= CpiSkuDimensionVariant.MaxAspectRatio;
    }
    
    public override string GetFailureMessage(CpiSkuDimensionVariant variant)
    {
        return $"Aspect ratio {variant.AspectRatio} exceeds the maximum allowed aspect ratio of {CpiSkuDimensionVariant.MaxAspectRatio}.";
    }
}