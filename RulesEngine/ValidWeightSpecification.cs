using System.Linq.Expressions;

namespace RulesEngine;

public class ValidWeightSpecification : Specification<CpiSkuDimensionVariant>
{
    public override Expression<Func<CpiSkuDimensionVariant, bool>> ToExpression()
    {
        return variant => variant.Weight <= CpiSkuDimensionVariant.MaxWeight;
    }
}