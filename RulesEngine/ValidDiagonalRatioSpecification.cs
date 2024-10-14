using System.Linq.Expressions;

namespace RulesEngine;

public class ValidDiagonalRatioSpecification : Specification<CpiSkuDimensionVariant>
{
    public override Expression<Func<CpiSkuDimensionVariant, bool>> ToExpression()
    {
        return variant => variant.DiagonalRatio <= CpiSkuDimensionVariant.MaxDiagonalRatio;
    }

    public override string GetFailureMessage(CpiSkuDimensionVariant variant)
    {
        return $"Diagonal ratio {variant.DiagonalRatio} exceeds the maximum allowed diagonal ratio of {CpiSkuDimensionVariant.MaxDiagonalRatio}.";
    }
}