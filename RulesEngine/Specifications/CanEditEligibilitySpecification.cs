using System.Linq.Expressions;
using RulesEngine.VariantStates;

namespace RulesEngine.Specifications;

public class CanEditEligibilitySpecification: Specification<CpiSkuDimensionVariant>
{
    public override Expression<Func<CpiSkuDimensionVariant, bool>> ToExpression()
    {
        return variant => variant.TippingState != TippingState.UnTippable;

    }
}