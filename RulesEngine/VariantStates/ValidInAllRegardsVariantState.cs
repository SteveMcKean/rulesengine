namespace RulesEngine.VariantStates;

public class ValidInAllRegardsVariantState : VariantStateBase
{
    public override void SetState(CpiSkuDimensionVariant variant)
    {
        IsSymboticEligible = true;
        CanEdit = true;
        ShowTipping = false;
        ShowUnTip = false;
        ShowExcluded = false;
        TippingState = TippingState.Unknown;
        TippingRecommendation = TippingRecommendation.NotAllowed;
        ChildVariants?.Clear();
    }
}