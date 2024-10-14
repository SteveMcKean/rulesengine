using System.Linq.Expressions;

namespace RulesEngine;

public class ValidWidthSpecification : Specification<CpiSkuDimensionVariant>
{
    public override Expression<Func<CpiSkuDimensionVariant, bool>> ToExpression()
    {
        return variant => variant.Width >= CpiSkuDimensionVariant.MinWidth && 
                          variant.Width <= CpiSkuDimensionVariant.MaxWidth;
    }
}