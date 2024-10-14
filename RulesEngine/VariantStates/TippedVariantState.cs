namespace RulesEngine.VariantStates;

public class TippedVariantState : VariantStateBase
{
    public override void SetState(CpiSkuDimensionVariant variant)
    {
        IsSymboticEligible = true;
        CanEdit = true;
        ShowTipping = true;
        ShowUnTip = true;
        ShowExcluded = false;
        TippingState = TippingState.Yes;
        TippingRecommendation = TippingRecommendation.Recommended;
        
        ChildVariants = GetChildVariants(variant);
    }

    private List<CpiSkuDimensionVariant> GetChildVariants(CpiSkuDimensionVariant variant)
    {
        var childVariants = new List<CpiSkuDimensionVariant>();
        
        foreach (var childVariant in variant.ChildVariants)
        {
            var childVariantState = new TippedVariantChildState();
            
            if (childVariant.StateManager.CanTransitionTo<TippedVariantChildState>())
                childVariant.StateManager.TransitionTo(childVariantState);
            else
                throw new InvalidOperationException("Cannot transition to the requested state.");
           
            childVariants.Add(childVariant);
            
        }
        
        return childVariants;
    }
}