using System.Linq.Expressions;

namespace RulesEngine;

public class ValidDiagonalRatioSpecification : Specification<CpiSkuDimensionVariant>
{
    public override Expression<Func<CpiSkuDimensionVariant, bool>> ToExpression()
    {
        return variant => variant.DiagonalRatio <= CpiSkuDimensionVariant.MaxDiagonalRatio;
    }
}