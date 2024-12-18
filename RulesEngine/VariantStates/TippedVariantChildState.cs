﻿namespace RulesEngine.VariantStates;

public class TippedVariantChildState : VariantStateBase
{
    public override void SetState(CpiSkuDimensionVariant variant)
    {
        IsSymboticEligible = true;
        CanEdit = false;
        ShowUnTip = true;
        ShowExcluded = false;
        TippingState = TippingState.Tipped;
        TippingRecommendation = TippingRecommendation.NotAllowed;
        ChildVariants?.Clear();
    }
}

public class ForcedVariantChildState : VariantStateBase
{
    public override void SetState(CpiSkuDimensionVariant variant)
    {
        IsSymboticEligible = false;
        CanEdit = false;
        ShowUnTip = false;
        ShowExcluded = false;
        TippingState = TippingState.Forced;
        TippingRecommendation = TippingRecommendation.Forced;
        
        ChildVariants = GetChildVariants(variant);
    }

    private static List<CpiSkuDimensionVariant> GetChildVariants(CpiSkuDimensionVariant variant)
    {  
        throw new NotImplementedException();
    }
}