using System.Linq.Expressions;

namespace RulesEngine;

public class ValidHeightSpecification : Specification<CpiSkuDimensionVariant>
{
    public override Expression<Func<CpiSkuDimensionVariant, bool>> ToExpression()
    {
        return variant => variant.Height >= CpiSkuDimensionVariant.MinHeight && 
                          variant.Height <= CpiSkuDimensionVariant.MaxHeight;
    }

    public override string GetFailureMessage(CpiSkuDimensionVariant entity)
    {
        return $"Height {entity.Height} is not within the valid range of {CpiSkuDimensionVariant.MinHeight} to {CpiSkuDimensionVariant.MaxHeight}";
    }
}