namespace RulesEngine.VariantStates;

public class AllowedNotRecommendedTippingVariantState : VariantStateBase
{
    public override void SetState(CpiSkuDimensionVariant variant)
    {
        IsSymboticEligible = true;
        CanEdit = true;
        ShowTipping = true;
        ShowUnTip = false;
        ShowExcluded = false;
        TippingState = TippingState.Unknown;
        TippingRecommendation = TippingRecommendation.AllowedNotRecommended;
        ChildVariants?.Clear();
    }
}