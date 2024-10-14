namespace RulesEngine;

public class RulesEngine
{
    
}

public interface ITippingRule
{
    TippingRecommendation Evaluate(CpiSkuDimensionVariant variant);
}

public enum TippingRecommendation
{
    NotAllowed,
    AllowedNotRecommended,
    Recommended,
    Forced,
    None
}

public class NotAllowedRecommendation: ITippingRule
{
    public TippingRecommendation Evaluate(CpiSkuDimensionVariant variant)
    {
        return TippingRecommendation.NotAllowed;
    }
}