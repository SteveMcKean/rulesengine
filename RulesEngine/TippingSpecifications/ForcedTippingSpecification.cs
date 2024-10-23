using System.Linq.Expressions;

namespace RulesEngine.TippingSpecifications;

public class ForcedTippingSpecification : Specification<CpiSkuDimensionVariant>
{
    public override Expression<Func<CpiSkuDimensionVariant, bool>> ToExpression()
    {
        return variant => variant.AspectRatio >= CpiSkuDimensionVariant.ForcedAspectRatio;
    }
}