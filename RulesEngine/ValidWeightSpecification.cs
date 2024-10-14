using System.Linq.Expressions;

namespace RulesEngine;

public class ValidWeightSpecification : Specification<CpiSkuDimensionVariant>
{
    public override Expression<Func<CpiSkuDimensionVariant, bool>> ToExpression()
    {
        return variant => variant.Weight >= CpiSkuDimensionVariant.MinWeight && 
                          variant.Weight <= CpiSkuDimensionVariant.MaxWeight;
    }

    public override string GetFailureMessage(CpiSkuDimensionVariant entity)
    {
        return $"Weight {entity.Weight} is not within the valid range of {CpiSkuDimensionVariant.MinWeight} to {CpiSkuDimensionVariant.MaxWeight}";
    }
}