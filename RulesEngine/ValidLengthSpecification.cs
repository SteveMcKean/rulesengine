using System.Linq.Expressions;

namespace RulesEngine;

public class ValidLengthSpecification : Specification<CpiSkuDimensionVariant>
{
    public override Expression<Func<CpiSkuDimensionVariant, bool>> ToExpression()
    {
        return variant => variant.Length >= CpiSkuDimensionVariant.MinLength && 
                          variant.Length <= CpiSkuDimensionVariant.MaxLength;
    }
}