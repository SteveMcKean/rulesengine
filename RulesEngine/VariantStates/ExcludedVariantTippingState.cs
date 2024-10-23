namespace RulesEngine.VariantStates;

public class ExcludedVariantTippingState : VariantStateBase
{
    public override void SetState(CpiSkuDimensionVariant variant)
    {
        IsSymboticEligible = false;
        CanEdit = false;
        ShowTipping = false;
        ShowUnTip = false;
        ShowExcluded = true;
        TippingState = TippingState.UnTippable;
        TippingRecommendation = TippingRecommendation.NotAllowed;
        ChildVariants?.Clear();
    }
}