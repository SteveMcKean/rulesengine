using System.Linq.Expressions;

namespace RulesEngine;

public class ValidLengthSpecification : Specification<CpiSkuDimensionVariant>
{
    public override Expression<Func<CpiSkuDimensionVariant, bool>> ToExpression()
    {
        return variant => variant.Length >= CpiSkuDimensionVariant.MinLength && 
                          variant.Length <= CpiSkuDimensionVariant.MaxLength;
    }

    public override string GetFailureMessage(CpiSkuDimensionVariant entity)
    {
        return $"Length {entity.Length} is not within the valid range of {CpiSkuDimensionVariant.MinLength} to {CpiSkuDimensionVariant.MaxLength}";
    }
}